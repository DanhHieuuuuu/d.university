using D.ApplicationBase;
using D.Auth.Domain.Dtos.Permission;
using D.Auth.Infrastructure.Services.Abstracts;

namespace D.Auth.Application.Query.Permission
{
    public class GetAllPermissionTreeHandler : IQueryHandler<PermissionTreeRequestDto, List<PermissionTreeResponseDto>>
    {
        private readonly IPermissionService _permissionService;

        public GetAllPermissionTreeHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<List<PermissionTreeResponseDto>> Handle(PermissionTreeRequestDto request, CancellationToken cancellationToken)
        {
            return _permissionService.GetPermissionTree(request);
        }
    }
}
