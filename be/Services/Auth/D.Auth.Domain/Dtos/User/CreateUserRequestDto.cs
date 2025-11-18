using D.DomainBase.Common;

namespace D.Auth.Domain.Dtos.User
{
    public class CreateUserRequestDto : ICommand<CreateUserResponseDto>
    {
        public string MaNhanSu { get; set; }
        public string? Email2 { get; set; }
        public string? Password { get; set; }
    }
}
