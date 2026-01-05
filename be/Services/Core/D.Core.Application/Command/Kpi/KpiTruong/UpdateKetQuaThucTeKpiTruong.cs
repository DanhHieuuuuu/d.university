using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiTruong;
using D.Core.Infrastructure.Services.Kpi.Abstracts;

namespace D.Core.Application.Command.Kpi.KpiTruong
{
    public class UpdateKetQuaThucTeKpiTruong : ICommandHandler<UpdateKpiThucTeKpiTruongListDto>
    {
        private readonly IKpiTruongService _service;

        public UpdateKetQuaThucTeKpiTruong(IKpiTruongService kpiTruongService)
        {
            _service = kpiTruongService;
        }

        public async Task Handle(UpdateKpiThucTeKpiTruongListDto request, CancellationToken cancellationToken)
        {
            await _service.UpdateKetQuaThucTe(request);
            return;
        }
    }
}
