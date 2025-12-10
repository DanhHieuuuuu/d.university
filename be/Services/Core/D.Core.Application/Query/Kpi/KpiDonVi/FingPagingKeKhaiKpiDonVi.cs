using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiDonVi;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using D.DomainBase.Dto;

namespace D.Core.Application.Query.Kpi.KpiDonVi
{
    public class FingPagingKeKhaiKpiDonVi : IQueryHandler<FilterKpiDonViKeKhaiDto, PageResultDto<KpiDonViDto>>
    {
        private readonly IKpiDonViService _service;

        public FingPagingKeKhaiKpiDonVi(IKpiDonViService kpiDonViService)
        {
            _service = kpiDonViService;
        }

        public async Task<PageResultDto<KpiDonViDto>> Handle(
            FilterKpiDonViKeKhaiDto request,
            CancellationToken cancellationToken
        )
        {
            return _service.FindPagingKeKhai(request);
        }
    }
}
