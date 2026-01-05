using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiCaNhan;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using D.DomainBase.Dto;

namespace D.Core.Application.Query.Kpi.KpiCaNhan
{
    public class FindPagingKpiKeKhaiCaNhan : IQueryHandler<FilterKpiKeKhaiCaNhanDto, PageResultDto<KpiCaNhanDto>>
    {
        private readonly IKpiCaNhanService _service;

        public FindPagingKpiKeKhaiCaNhan(IKpiCaNhanService kpiCaNhanService)
        {
            _service = kpiCaNhanService;
        }

        public Task<PageResultDto<KpiCaNhanDto>> Handle(
            FilterKpiKeKhaiCaNhanDto request,
            CancellationToken cancellationToken
        )
        {
            return _service.FindPagingKpiCaNhanKeKhai(request);
        }
    }
}
