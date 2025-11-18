using D.DomainBase.Common;

namespace D.Auth.Domain.Dtos.User.Password
{
    public class ChangePasswordRequestDto : ICommand<bool>
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
