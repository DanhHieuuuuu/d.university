using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiCaNhan;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using D.DomainBase.Dto;

namespace D.Core.Application.Query.Kpi.KpiCaNhan
{
    public class FindPagingKpiCaNhan : IQueryHandler<FilterKpiCaNhanDto, PageResultDto<KpiCaNhanDto>>
    {
        private readonly IKpiCaNhanService _service;

        public FindPagingKpiCaNhan(IKpiCaNhanService kpiCaNhanService)
        {
            _service = kpiCaNhanService;
        }

        public Task<PageResultDto<KpiCaNhanDto>> Handle(
            FilterKpiCaNhanDto request,
            CancellationToken cancellationToken
        )
        {
            return _service.FindPagingKpiCaNhan(request);
        }
    }
}
