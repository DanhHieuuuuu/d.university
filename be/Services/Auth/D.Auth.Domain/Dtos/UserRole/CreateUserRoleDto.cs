using D.DomainBase.Common;

namespace D.Auth.Domain.Dtos.UserRole
{
    public class CreateUserRoleDto : ICommand<bool>
    {
        public int NhanSuId { get; set; }
        public int RoleId { get; set; }
    }
}
