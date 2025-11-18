using D.DomainBase.Common;

namespace D.Auth.Domain.Dtos.User
{
    public class UpdateUserRequestDto : ICommand<bool>
    {
        public int Id { get; set; }
        public string? NewPassword { get; set; }

        public string? Email { get; set; }
    }
}
