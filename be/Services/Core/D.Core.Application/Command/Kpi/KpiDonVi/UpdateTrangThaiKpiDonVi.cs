using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiDonVi;
using D.Core.Infrastructure.Services.Kpi.Abstracts;

namespace D.Core.Application.Command.Kpi.KpiDonVi
{
    public class UpdateTrangThaiKpiDonVi : ICommandHandler<UpdateTrangThaiKpiDonViDto>
    {
        private readonly IKpiDonViService _service;

        public UpdateTrangThaiKpiDonVi(IKpiDonViService kpiDonViService)
        {
            _service = kpiDonViService;
        }

        public async Task Handle(UpdateTrangThaiKpiDonViDto request, CancellationToken cancellationToken)
        {
            await _service.UpdateTrangThaiKpiDonVi(request);
            return;
        }
    }
}
