using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiDonVi;
using D.Core.Infrastructure.Services.Kpi.Abstracts;

namespace D.Core.Application.Command.Kpi.KpiDonVi
{
    public class GiaoKpiDonVi : ICommandHandler<GiaoKpiDonViDto>
    {
        private readonly IKpiDonViService _service;

        public GiaoKpiDonVi(IKpiDonViService kpiDonViService)
        {
            _service = kpiDonViService;
        }

        public async Task Handle(GiaoKpiDonViDto request, CancellationToken cancellationToken)
        {
            await _service.GiaoKpiDonVi(request);
        }
    }
}
