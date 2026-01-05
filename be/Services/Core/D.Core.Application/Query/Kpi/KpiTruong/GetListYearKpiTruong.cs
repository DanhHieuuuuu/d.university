using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiTruong;
using D.Core.Infrastructure.Services.Kpi.Abstracts;

namespace D.Core.Application.Query.Kpi.KpiTruong
{
    public class GetListYearKpiTruong : IQueryHandler<GetListYearKpiTruongRequestDto, List<GetListYearKpiTruongDto>>
    {
        private readonly IKpiTruongService _service;

        public GetListYearKpiTruong(IKpiTruongService kpiTruongService)
        {
            _service = kpiTruongService;
        }

        public Task<List<GetListYearKpiTruongDto>> Handle(GetListYearKpiTruongRequestDto request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_service.GetListYear());
        }
    }
}
