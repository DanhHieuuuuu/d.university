using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiTruong;
using D.Core.Infrastructure.Services.Kpi.Abstracts;

namespace D.Core.Application.Command.Kpi.KpiTruong
{
    public class UpdateKpiTruong : ICommandHandler<UpdateKpiTruongDto>
    {
        private readonly IKpiTruongService _service;

        public UpdateKpiTruong(IKpiTruongService kpiTruongService)
        {
            _service = kpiTruongService;
        }

        public async Task Handle(UpdateKpiTruongDto request, CancellationToken cancellationToken)
        {
            await _service.UpdateKpiTruong(request);
            return;
        }
    }
}
