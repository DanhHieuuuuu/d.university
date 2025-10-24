using D.ApplicationBase;
using D.Auth.Domain.Dtos.Permission;
using D.Auth.Infrastructure.Services.Abstracts;

namespace D.Auth.Application.Query.Permission
{
    public class GetPermisisonNhanSu : IQueryHandler<GetPermissionNhanSuDto, List<string>>
    {
        private readonly IPermissionService _perrmissionService;

        public GetPermisisonNhanSu(IPermissionService permissionService)
        {
            _perrmissionService = permissionService;
        }

        public async Task<List<string>> Handle(
            GetPermissionNhanSuDto request,
            CancellationToken cancellationToken
        )
        {
            return _perrmissionService.GetPermissionsByNhanSu(request);
        }
    }
}
