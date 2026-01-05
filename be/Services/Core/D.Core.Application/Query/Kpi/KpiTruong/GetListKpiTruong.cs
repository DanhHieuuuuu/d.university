using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiTruong;
using D.Core.Infrastructure.Services.Kpi.Abstracts;

namespace D.Core.Application.Query.Kpi.KpiTruong
{
    public class GetListKpiTruong : IQueryHandler<GetListKpiTruongRequestDto, List<GetListKpiTruongResponseDto>>
    {
        private readonly IKpiTruongService _service;

        public GetListKpiTruong(IKpiTruongService kpiTruongService)
        {
            _service = kpiTruongService;
        }

        public Task<List<GetListKpiTruongResponseDto>> Handle(
            GetListKpiTruongRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return Task.FromResult(_service.GetListKpiTruong());
        }
    }
}
