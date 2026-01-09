using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiDonVi;
using D.Core.Infrastructure.Services.Kpi.Abstracts;


namespace D.Core.Application.Query.Kpi.KpiDonVi
{
    public class GetKeKhaiTimeDonVi : IQueryHandler<KpiKeKhaiTimeDonViRequestDto, KpiKeKhaiTimeDonViDto>
    {
        private readonly IKpiDonViService _service;

        public GetKeKhaiTimeDonVi(IKpiDonViService kpiDonViService)
        {
            _service = kpiDonViService;
        }

        public Task<KpiKeKhaiTimeDonViDto> Handle(KpiKeKhaiTimeDonViRequestDto request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_service.GetKpiKeKhaiTime());
        }
    }
}
