using D.DomainBase.Common;

namespace D.Auth.Domain.Dtos.User
{
    public class CreateUserRequestDto2 : ICommand<CreateUserResponseDto2>
    {

        public string MaNhanSu { get; set; }
        public string? Email2 { get; set; }
        public string? Password { get; set; }
        public string? hoDem { get; set; }
        public string? ten { get; set; }
    }
}
