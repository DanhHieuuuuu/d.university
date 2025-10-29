using D.ApplicationBase;
using D.Auth.Domain.Dtos.Role;
using D.Auth.Infrastructure.Services.Abstracts;

namespace D.Auth.Application.Command.Role
{
    public class DeleteRole : ICommandHandler<DeleteRoleDto>
    {
        private readonly IRoleService _roleService;

        public DeleteRole(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task Handle(DeleteRoleDto request, CancellationToken cancellationToken)
        {
            _roleService.DeleteRole(request.RoleId);
            return;
        }
    }
}
