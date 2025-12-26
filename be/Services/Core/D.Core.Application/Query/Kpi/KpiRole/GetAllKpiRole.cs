using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiRole;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using D.DomainBase.Dto;

namespace D.Core.Application.Query.Kpi.KpiRole
{
    public class GetAllKpiRole : IQueryHandler<KpiRoleRequestDto, PageResultDto<KpiRoleResponseDto>>
    {
        private readonly IKpiRoleService _service;

        public GetAllKpiRole(IKpiRoleService kpiRoleService)
        {
            _service = kpiRoleService;
        }

        public async Task<PageResultDto<KpiRoleResponseDto>> Handle(
            KpiRoleRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return _service.FindAllKpiRole(request);
        }
    }
}
