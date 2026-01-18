using D.ApplicationBase; 
using D.Core.Domain.Dtos.Kpi.KpiTinhDiem.D.Core.Domain.Dtos.Kpi.KpiTinhDiem;
using D.Core.Infrastructure.Services.Kpi.Abstracts;


namespace D.Core.Application.Query.Kpi.KpiCaNhan
{
    public class GetKpiDashboardHandler : IQueryHandler<GetKpiScoreBoardDto, KpiDashboardResponse>
    {
        private readonly IKpiTinhDiemService _service;

        public GetKpiDashboardHandler(IKpiTinhDiemService service)
        {
            _service = service;
        }

        public Task<KpiDashboardResponse> Handle(GetKpiScoreBoardDto request, CancellationToken cancellationToken)
        {
            return _service.GetDashboardData(request);
        }
    }
}