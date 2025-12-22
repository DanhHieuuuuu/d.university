using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiTruong;
using D.Core.Infrastructure.Services.Kpi.Abstracts;

namespace D.Core.Application.Command.Kpi.KpiTruong
{
    public class UpdateTrangThaiKpiTruong : ICommandHandler<UpdateTrangThaiKpiTruongDto>
    {
        private readonly IKpiTruongService _service;

        public UpdateTrangThaiKpiTruong(IKpiTruongService kpiTruongService)
        {
            _service = kpiTruongService;
        }

        public async Task Handle(UpdateTrangThaiKpiTruongDto request, CancellationToken cancellationToken)
        {
            await _service.UpdateTrangThaiKpiTruong(request);
        }
    }
}
