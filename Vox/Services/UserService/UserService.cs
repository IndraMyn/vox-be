using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Pkcs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vox.Configurations;
using Vox.Data.UserData;
using Vox.Models;
using Vox.Utils;

namespace Vox.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;

        private readonly DBContext _dbContext;
        private JWTOptions _jwtOptions;

        public UserService(ILogger<UserService> logger, DBContext dBContext, IOptions<JWTOptions> jwtOptions)
        {
            _logger = logger;
            _dbContext = dBContext;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task changePassword(long id, UserChangePasswordRequest request)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                _logger.LogWarning($"User with {id} is not found");

                throw new ErrorException(new Error()
                {
                    message = $"No query results for model [App\\User] {id}",
                    statusCode = 404
                });
            }

            var verify = SecurePasswordHasher.Verify(request.Password, user.Password);
            if (!verify)
            {
                throw new ErrorException(new Error()
                {
                    message = $"Wrong Password.",
                    statusCode = 401
                });
            }

            if (request.NewPassword != request.RepeatPassword)
            {
                throw new ErrorException(new Error()
                {
                    message = $"Password must same with repeat password.",
                    statusCode = 422
                });
            }

            user.Password= SecurePasswordHasher.Hash(request.NewPassword);
            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<UserDetailResponse> create(UserCreateRequest request)
        {

            if (request.Password != request.RepeatPassword)
            {
                throw new ErrorException(new Error()
                {
                    message = $"Password must same with repeat password.",
                    statusCode = 422
                });
            }

            var check = await _dbContext.Users.Where(x => x.Email == request.Email).AnyAsync();
            if (check)
            {
                throw new ErrorException(new Error()
                {
                    message = $"The email has already been taken.",
                    statusCode = 422
                });
            }

            var user = new UserModel()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = SecurePasswordHasher.Hash(request.Password),
            };

            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return new UserDetailResponse()
            {
                id = user.ID,
                email = user.Email,
                firstName = user.FirstName,
                lastName = user.LastName,
            };
        }

        public async Task delete(long id)
        {
            var user = await _dbContext.Users.FindAsync(id);

            if (user == null)
            {
                throw new ErrorException(new Error()
                {
                    message = $"No query results for model [App\\User] " + id,
                    statusCode = 404
                });
            }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<UserDetailResponse> findUser(long id)
        {

            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                _logger.LogWarning($"User with {id} is not found");

                throw new ErrorException(new Error()
                {
                    message = $"No query results for model [App\\User] {id}",
                    statusCode = 404
                });
            }

            return new UserDetailResponse() 
            {
                id= id,
                email = user.Email,
                firstName= user.FirstName,
                lastName= user.LastName,
            };
        }

        public async Task<UserLoginResponse> login(UserLoginRequest request)
        {
            var user = await _dbContext.Users.Where(x => x.Email == request.Email).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new ErrorException(new Error()
                {
                    message = $"Invalid Credentials",
                    statusCode = 401
                });
            }

            var verify = SecurePasswordHasher.Verify(request.Password, user.Password);
            if (!verify)
            {
                throw new ErrorException(new Error()
                {
                    message = $"Invalid Credentials",
                    statusCode = 401
                });
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.JWTSecret);
            var expires = DateTime.UtcNow.AddMinutes(_jwtOptions.TokenDuration);

            var claims = new List<Claim>() {
                new Claim(JwtRegisteredClaimNames.NameId, user.Email),
            };

            var jwtToken = new JwtSecurityToken
            (
                claims: claims,
                expires: expires,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            );

            var token = tokenHandler.WriteToken(jwtToken);

            return new UserLoginResponse()
            {
                ID = user.ID,
                Email = user.Email,
                Token = token,
            };
        }

        public async Task<UserDetailResponse> update(long id, UserUpdateRequests request)
        {
            var user = await _dbContext.Users.FindAsync(id);

            if (user == null)
            {
                throw new ErrorException(new Error()
                {
                    message = $"No query results for model [App\\User] " + id,
                    statusCode = 404
                });
            }

            var checkEmail = await _dbContext.Users.Where(x => x.ID != user.ID && x.Email == user.Email).AnyAsync();
            if (checkEmail)
            {
                throw new ErrorException(new Error()
                {
                    message = $"The email has already been taken.",
                    statusCode = 422
                });
            }

            user.FirstName= request.FirstName;
            user.LastName= request.LastName;
            user.Email= request.Email;

            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();

            return new UserDetailResponse()
            {
                id = user.ID,
                firstName = user.FirstName,
                lastName = user.LastName,
            };
        }
    }
}
