using D.DomainBase.Common;

namespace D.Auth.Domain.Dtos.User.Password
{
    public class ResetPasswordRequestDto : ICommand<bool>
    {
        public string MaNhanSu { get; set; }
    }
}
