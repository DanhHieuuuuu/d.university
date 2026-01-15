using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiCaNhan;
using D.Core.Infrastructure.Services.Kpi.Abstracts;

namespace D.Core.Application.Command.Kpi.KpiCaNhan
{
    public class UpdateTrangThai : ICommandHandler<UpdateTrangThaiKpiDto>
    {
        private readonly IKpiCaNhanService _service;

        public UpdateTrangThai(IKpiCaNhanService kpiCaNhanService)
        {
            _service = kpiCaNhanService;
        }

        public async Task Handle(UpdateTrangThaiKpiDto request, CancellationToken cancellationToken)
        {
            await _service.UpdateTrangThaiKpiCaNhan(request);
        }
    }
}
