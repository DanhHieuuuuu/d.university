using D.DomainBase.Common;

namespace D.Auth.Domain.Dtos.Role
{
    public class UpdateRoleDto : ICommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
