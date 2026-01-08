using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiDonVi;
using D.Core.Infrastructure.Services.Kpi.Abstracts;

namespace D.Core.Application.Command.Kpi.KpiDonVi
{
    public class UpdateKetQuaCapTrenKpiDonVi : ICommandHandler<UpdateKetQuaCapTrenKpiDonViListDto>
    {
        private readonly IKpiDonViService _service;

        public UpdateKetQuaCapTrenKpiDonVi(IKpiDonViService kpiDonViService)
        {
            _service = kpiDonViService;
        }

        public async Task Handle(UpdateKetQuaCapTrenKpiDonViListDto request, CancellationToken cancellationToken)
        {
            _service.UpdateKetQuaCapTren(request);
            return;
        }
    }
}
