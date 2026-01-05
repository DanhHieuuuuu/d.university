using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiCaNhan;
using D.Core.Infrastructure.Services.Kpi.Abstracts;

namespace D.Core.Application.Command.Kpi.KpiCaNhan
{
    public class UpdateKetQuaCapTren : ICommandHandler<UpdateKetQuaCapTrenKpiCaNhanListDto>
    {
        private readonly IKpiCaNhanService _service;

        public UpdateKetQuaCapTren(IKpiCaNhanService kpiCaNhanService)
        {
            _service = kpiCaNhanService;
        }

        public async Task Handle(UpdateKetQuaCapTrenKpiCaNhanListDto request, CancellationToken cancellationToken)
        {
            _service.UpdateKetQuaCapTren(request);
            return;
        }
    }
}
