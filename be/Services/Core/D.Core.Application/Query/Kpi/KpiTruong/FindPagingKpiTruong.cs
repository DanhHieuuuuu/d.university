using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiTruong;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using D.DomainBase.Dto;

namespace D.Core.Application.Query.Kpi.KpiTruong
{
    public class FindPagingKpiTruong : IQueryHandler<FilterKpiTruongDto, PageResultDto<KpiTruongDto>>
    {
        private readonly IKpiTruongService _service;

        public FindPagingKpiTruong(IKpiTruongService kpiTruongService)
        {
            _service = kpiTruongService;
        }

        public async Task<PageResultDto<KpiTruongDto>> Handle(
            FilterKpiTruongDto request,
            CancellationToken cancellationToken
        )
        {
            return _service.GetAllKpiTruong(request);
        }
    }
}
