
using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiCaNhan;
using D.Core.Infrastructure.Services.Kpi.Abstracts;

namespace D.Core.Application.Query.Kpi.KpiDonVi
{
    public class GetKeKhaiCaNhanTime : IQueryHandler<KpiKeKhaiTimeCaNhanRequestDto, KpiKeKhaiTimeCaNhanDto>
    {
        private readonly IKpiCaNhanService _service;

        public GetKeKhaiCaNhanTime(IKpiCaNhanService kpiCaNhanService)
        {
            _service = kpiCaNhanService;
        }

        public Task<KpiKeKhaiTimeCaNhanDto> Handle(KpiKeKhaiTimeCaNhanRequestDto request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_service.GetKpiKeKhaiTime());
        }
    }
}
