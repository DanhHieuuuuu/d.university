using D.DomainBase.Common;

namespace D.Auth.Domain.Dtos.Role
{
    public class UpdateRoleStatusDto : ICommand
    {
        public int Id { get; set; }
        public int Status { get; set; }
    }
}
