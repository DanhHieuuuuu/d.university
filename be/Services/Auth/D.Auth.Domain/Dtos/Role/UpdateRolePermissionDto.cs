using D.DomainBase.Common;

namespace D.Auth.Domain.Dtos.Role
{
    public class UpdateRolePermissionDto : ICommand
    {
        public int RoleId { get; set; }
        public List<int> PermissionIds { get; set; }
    }
}
