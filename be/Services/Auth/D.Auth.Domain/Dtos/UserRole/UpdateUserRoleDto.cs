using D.DomainBase.Common;

namespace D.Auth.Domain.Dtos.UserRole
{
    public class UpdateUserRoleDto : ICommand<bool>
    {
        public int NhanSuId { get; set; }
        public List<int>? RoleIds { get; set; }
    }
}
