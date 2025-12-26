using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiDonVi;
using D.Core.Infrastructure.Services.Kpi.Abstracts;

namespace D.Core.Application.Command.Kpi.KpiDonVi
{
    public class UpdateKpiDonVi : ICommandHandler<UpdateKpiDonViDto>
    {
        private readonly IKpiDonViService _service;

        public UpdateKpiDonVi(IKpiDonViService kpiDonViService)
        {
            _service = kpiDonViService;
        }

        public async Task Handle(UpdateKpiDonViDto request, CancellationToken cancellationToken)
        {
            _service.UpdateKpiDonVi(request);
            return;
        }
    }
}
