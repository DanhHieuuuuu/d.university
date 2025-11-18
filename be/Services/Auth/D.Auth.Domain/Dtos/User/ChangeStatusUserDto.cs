using D.DomainBase.Common;

namespace D.Auth.Domain.Dtos.User
{
    public class ChangeStatusUserDto : ICommand<bool>
    {
        public int NhanSuId { get; set; }
    }
}
