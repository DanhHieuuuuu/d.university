using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiRole;
using D.Core.Infrastructure.Services.Kpi.Abstracts;

namespace D.Core.Application.Query.Kpi.KpiDonVi
{
    public class GetListKpiRoleByUser : IQueryHandler<GetKpiRoleByUserRequestDto, List<GetKpiRoleByUserResponseDto>>
    {
        private readonly IKpiRoleService _service;

        public GetListKpiRoleByUser(IKpiRoleService kpiRoleService)
        {
            _service = kpiRoleService;
        }

        public  Task<List<GetKpiRoleByUserResponseDto>> Handle(GetKpiRoleByUserRequestDto request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_service.GetRoleByUserId());
        }
    }
}
