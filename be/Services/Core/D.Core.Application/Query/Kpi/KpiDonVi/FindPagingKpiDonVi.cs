using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiDonVi;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using D.DomainBase.Dto;


namespace D.Core.Application.Query.Kpi.KpiDonVi
{
    public class FindPagingKpiDonVi : IQueryHandler<FilterKpiDonViDto, PageResultDto<KpiDonViDto>>
    {
        private readonly IKpiDonViService _service;

        public FindPagingKpiDonVi(IKpiDonViService kpiDonViService)
        {
            _service = kpiDonViService;
        }

        public async Task<PageResultDto<KpiDonViDto>> Handle(
            FilterKpiDonViDto request,
            CancellationToken cancellationToken
        )
        {
            return _service.GetAllKpiDonVi(request);
        }
    }
}
