using D.DomainBase.Common;

namespace D.Auth.Domain.Dtos.Login
{
    public class RefreshTokenRequestDto : ICommand<RefreshTokenResponseDto>
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
