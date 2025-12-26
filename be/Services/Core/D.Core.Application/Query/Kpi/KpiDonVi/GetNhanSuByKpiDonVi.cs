using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiDonVi;
using D.Core.Infrastructure.Services.Kpi.Abstracts;

namespace D.Core.Application.Query.Kpi.KpiDonVi
{
    public class GetNhanSuByKpiDonVi : IQueryHandler<GetNhanSuFromKpiDonViDto, List<NhanSuDaGiaoDto>>
    {
        private readonly IKpiDonViService _service;

        public GetNhanSuByKpiDonVi(IKpiDonViService kpiDonViService)
        {
            _service = kpiDonViService;
        }

        public async Task<List<NhanSuDaGiaoDto>> Handle(GetNhanSuFromKpiDonViDto request, CancellationToken cancellationToken)
        {
            return _service.GetNhanSuByKpiDonVi(request);
        }
    }
}
