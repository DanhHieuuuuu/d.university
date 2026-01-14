using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiTruong;
using D.Core.Infrastructure.Services.Kpi.Abstracts;

namespace D.Core.Application.Command.Kpi.KpiTruong
{
    public class UpdateKetQuaCapTrenKpiTruong : ICommandHandler<UpdateKetQuaCapTrenKpiTruongListDto>
    {
        private readonly IKpiTruongService _service;

        public UpdateKetQuaCapTrenKpiTruong(IKpiTruongService kpiTruongService)
        {
            _service = kpiTruongService;
        }

        public async Task Handle(UpdateKetQuaCapTrenKpiTruongListDto request, CancellationToken cancellationToken)
        {
            _service.UpdateKetQuaCapTren(request);
            return;
        }
    }
}
