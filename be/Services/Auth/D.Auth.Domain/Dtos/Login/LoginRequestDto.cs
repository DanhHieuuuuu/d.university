using D.DomainBase.Common;

namespace D.Auth.Domain.Dtos.Login
{
    public class LoginRequestDto : IQuery<LoginResponseDto>
    {
        public string MaNhanSu { get; set; }
        public string Password { get; set; }
    }
}
