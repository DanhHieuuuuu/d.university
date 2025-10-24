using D.ApplicationBase;
using D.Auth.Domain.Dtos.Role;
using D.Auth.Infrastructure.Services.Abstracts;

namespace D.Auth.Application.Command.Role
{
    public class CreateRole : ICommandHandler<CreateRoleRequestDto, bool>
    {
        private readonly IRoleService _roleService;

        public CreateRole(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<bool> Handle(CreateRoleRequestDto request, CancellationToken cancellationToken)
        {
            return _roleService.CreateRole(request);
        }
    }
}
