using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiCaNhan;
using D.Core.Infrastructure.Services.Kpi.Abstracts;


namespace D.Core.Application.Command.Kpi.KpiCaNhan
{
    public class UpdateKetQuaThucTe : ICommandHandler<UpdateKpiThucTeKpiCaNhanListDto>
    {
        private readonly IKpiCaNhanService _service;

        public UpdateKetQuaThucTe(IKpiCaNhanService kpiCaNhanService)
        {
            _service = kpiCaNhanService;
        }

        public async Task Handle(UpdateKpiThucTeKpiCaNhanListDto request, CancellationToken cancellationToken)
        {
            _service.UpdateKetQuaThucTe(request);
            return;
        }
    }
}
