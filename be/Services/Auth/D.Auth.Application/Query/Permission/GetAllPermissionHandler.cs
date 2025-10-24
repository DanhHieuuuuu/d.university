using D.ApplicationBase;
using D.Auth.Domain.Dtos.Permission;
using D.Auth.Infrastructure.Services.Abstracts;

namespace D.Auth.Application.Query.Permisison
{
    public class GetAllPermissionHandler
        : IQueryHandler<PermissionRequestDto, List<PermissionResponseDto>>
    {
        public IPermissionService _permissionService { get; set; }

        public GetAllPermissionHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<List<PermissionResponseDto>> Handle(
            PermissionRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return _permissionService.GetAllPermission(request);
        }
    }
}
