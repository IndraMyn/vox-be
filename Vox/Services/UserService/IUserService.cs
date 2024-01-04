using Vox.Data.UserData;

namespace Vox.Services.UserService
{
    public interface IUserService
    {
        Task<UserLoginResponse> login(UserLoginRequest request);
        Task<UserDetailResponse> create(UserCreateRequest request);
        Task<UserDetailResponse> findUser(long id);
        Task<UserDetailResponse> update(long id, UserUpdateRequests request);
        Task changePassword(long id, UserChangePasswordRequest request);
        Task delete(long id);

    }
}
