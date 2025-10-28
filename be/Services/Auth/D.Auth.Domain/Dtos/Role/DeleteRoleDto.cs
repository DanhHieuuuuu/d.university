using D.DomainBase.Common;

namespace D.Auth.Domain.Dtos.Role
{
    public class DeleteRoleDto : ICommand
    {
        public int RoleId { get; set; }
    }
}
