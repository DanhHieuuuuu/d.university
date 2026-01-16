using D.ApplicationBase;
using D.Auth.Domain.Dtos.Role;
using D.Auth.Infrastructure.Services.Abstracts;

namespace D.Auth.Application.Command.Role
{
    public class UpdateRoleStatusHandler : ICommandHandler<UpdateRoleStatusDto>
    {
        private readonly IRoleService _service;

        public UpdateRoleStatusHandler(IRoleService service)
        {
            _service = service;
        }

        public async Task Handle(UpdateRoleStatusDto request, CancellationToken cancellationToken)
        {
            _service.UpdateStatusRole(request);
            return;
        }
    }
}
