using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiCongThuc;
using D.Core.Infrastructure.Services.Kpi.Abstracts;


namespace D.Core.Application.Query.Kpi.KpiCongThuc
{
    public class GetAllKpiCongThuc : IQueryHandler<GetCongThucRequestDto, List<KpiCongThucDto>>
    {
        private readonly IKpiCongThucService _service;

        public GetAllKpiCongThuc(IKpiCongThucService kpiCongThucService)
        {
            _service = kpiCongThucService;
        }

        public async Task<List<KpiCongThucDto>> Handle(
            GetCongThucRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return await _service.GetAllKpiCongThuc(request);
        }
    }
}
