using D.ApplicationBase;
using D.Auth.Domain.Dtos.Role;
using D.Auth.Infrastructure.Services.Abstracts;

namespace D.Auth.Application.Command.Role
{
    public class UpdateRolePermission : ICommandHandler<UpdateRolePermissionDto>
    {
        public IRoleService _roleService { get; set; }

        public UpdateRolePermission(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task Handle(
            UpdateRolePermissionDto request,
            CancellationToken cancellationToken
        )
        {
            _roleService.UpdateRolePermission(request);
            return;
        }
    }
}
