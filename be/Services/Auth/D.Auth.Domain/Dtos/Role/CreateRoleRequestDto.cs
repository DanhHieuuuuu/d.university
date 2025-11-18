using D.DomainBase.Common;

namespace D.Auth.Domain.Dtos.Role
{
    public class CreateRoleRequestDto : ICommand<bool>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
