using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiTruong;
using D.Core.Infrastructure.Services.Kpi.Abstracts;

namespace D.Core.Application.Command.Kpi.KpiTruong
{
    public class GiaoKpiHieuTruong : ICommandHandler<GiaoKpiHieuTruongDto>
    {
        private readonly IKpiTruongService _service;

        public GiaoKpiHieuTruong(IKpiTruongService kpiTruongService)
        {
            _service = kpiTruongService;
        }

        public async Task Handle(GiaoKpiHieuTruongDto request, CancellationToken cancellationToken)
        {
            await _service.GiaoKpiHieuTruong(request);
        }
    }
}
