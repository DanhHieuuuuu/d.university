using D.Auth.Domain.Dtos;
using D.Auth.Domain.Dtos.Login;
using D.Auth.Domain.Dtos.UserRole;
using D.DomainBase.Dto;

namespace D.Auth.Infrastructure.Services.Abstracts
{
    public interface INsNhanSuService
    {
        bool AddUserRole(CreateUserRoleDto createUserRole);
        PageResultDto<NsNhanSuResponseDto> FindPagingNsNhanSu(NsNhanSuRequestDto dto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequest);
        Task<bool> Logout(LogoutRequestDto logoutRequestDto);
        Task<RefreshTokenResponseDto> RefreshToken(RefreshTokenRequestDto refreshToken);
        Task<bool> ValidateTokenAsync();
    }
}
