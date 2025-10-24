using D.ApplicationBase;
using D.Auth.Domain.Dtos.Permission;
using D.Auth.Infrastructure.Services.Abstracts;

namespace D.Auth.Application.Command.Permission
{
    public class ImportPermissionHandler : ICommandHandler<ImportPermissionCommand>
    {
        public IPermissionService _permissionService { get; set; }

        public ImportPermissionHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task Handle(ImportPermissionCommand req, CancellationToken cancellationToken)
        {
            await _permissionService.ImportPermission(req);
            return;
        }
    }
}
