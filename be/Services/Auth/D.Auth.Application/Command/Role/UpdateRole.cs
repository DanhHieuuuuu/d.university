using D.ApplicationBase;
using D.Auth.Domain.Dtos.Role;
using D.Auth.Infrastructure.Services.Abstracts;

namespace D.Auth.Application.Command.Role
{
    public class UpdateRole : ICommandHandler<UpdateRoleDto>
    {
        public IRoleService _roleService { get; set; }

        public UpdateRole(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task Handle(UpdateRoleDto request, CancellationToken cancellationToken)
        {
            _roleService.UpdateRole(request);
            return;
        }
    }
}
