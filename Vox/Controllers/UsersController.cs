using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vox.Data.UserData;
using Vox.Services.UserService;
using Vox.Utils;

namespace Vox.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UsersController : ControllerBase
    {

        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;
        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost]
        [Produces("application/json")]
        [Route("")]
        [ProducesResponseType(typeof(UserDetailResponse), 200)]
        public async Task<IActionResult> Create([FromBody] UserCreateRequest request)
        {
            try
            {
                return Ok(await _userService.create(request));
            }
            catch (ErrorException e)
            {
                return StatusCode(e.Error.statusCode, e.Error);
            }
        }

        [Authorize]
        [HttpGet]
        [Produces("application/json")]
        [Route("{id}")]
        [ProducesResponseType(typeof(UserDetailResponse), 200)]
        public async Task<IActionResult> Detail(long id)
        {
            try
            {
                return Ok(await _userService.findUser(id));
            }
            catch (ErrorException e)
            {
                return StatusCode(e.Error.statusCode, e.Error);
            }
        }

        [Authorize]
        [HttpPut]
        [Produces("application/json")]
        [Route("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UserUpdateRequests request)
        {
            try
            {
                return Ok(await _userService.update(id, request));
            }
            catch (ErrorException e)
            {
                return StatusCode(e.Error.statusCode, e.Error);
            }
        }

        [Authorize]
        [HttpDelete]
        [Produces("application/json")]
        [Route("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _userService.delete(id);
                return Ok();
            }
            catch (ErrorException e)
            {
                return StatusCode(e.Error.statusCode, e.Error);
            }
        }

        [HttpPut]
        [Produces("application/json")]
        [Route("{id}/password")]
        public async Task<IActionResult> ChangePassword(long id, [FromBody] UserChangePasswordRequest request)
        {
            try
            {
                await _userService.changePassword(id, request);
                return Ok();
            }
            catch (ErrorException e)
            {
                return StatusCode(e.Error.statusCode, e.Error);
            }
        }

        [HttpPost]
        [Produces("application/json")]
        [Route("login")]
        [ProducesResponseType(typeof(UserDetailResponse), 200)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            try
            {
                return Ok(await _userService.login(request));
            }
            catch (ErrorException e)
            {
                return StatusCode(e.Error.statusCode, e.Error);
            }
        }
    }
}
