using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiDonVi;
using D.Core.Infrastructure.Services.Kpi.Abstracts;

namespace D.Core.Application.Command.Kpi.KpiDonVi
{
    public class UpdateKetQuaThucTeKpiDonVi : ICommandHandler<UpdateKpiThucTeKpiDonViListDto>
    {
        private readonly IKpiDonViService _service;

        public UpdateKetQuaThucTeKpiDonVi(IKpiDonViService kpiDonViService)
        {
            _service = kpiDonViService;
        }

        public async Task Handle(UpdateKpiThucTeKpiDonViListDto request, CancellationToken cancellationToken)
        {
            _service.UpdateKetQuaThucTe(request);
            return;
        }
    }
}
