using D.Auth.Domain.Dtos.User;
using D.Auth.Domain.Dtos.User.Password;
using D.Auth.Domain.Dtos.UserRole;

namespace D.Auth.Infrastructure.Services.Abstracts
{
    public interface IUserService
    {
        bool ChangePassword(ChangePasswordRequestDto request);
        Task<CreateUserResponseDto> CreateUser(CreateUserRequestDto request);
        Task<bool> UpdateUser(UpdateUserRequestDto request);
        Task<bool> ResetPasswordAsync(ResetPasswordRequestDto request);
        Task<Stream> UpdateUserImage(UpdateImageUserDto request);
        Task<GetUserRolesByUserIdResponseDto> GetUserRolesByUserId(int nhanSuId);
        Task<bool> UpdateUserRoles(UpdateUserRoleDto dto);
        Task<bool> ChangeStatusUser(int nhanSuId);
        Task<CreateUserResponseDto2> CreateUser2(CreateUserRequestDto2 request);
    }
}
