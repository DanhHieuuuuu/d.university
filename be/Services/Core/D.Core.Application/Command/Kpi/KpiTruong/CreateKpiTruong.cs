using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiTruong;
using D.Core.Infrastructure.Services.Kpi.Abstracts;


namespace D.Core.Application.Command.Kpi.KpiTruong
{
    public class CreateKpiTruong : ICommandHandler<CreateKpiTruongDto>
    {
        private readonly IKpiTruongService _service;

        public CreateKpiTruong(IKpiTruongService kpiDonViService)
        {
            _service = kpiDonViService;
        }

        public async Task Handle(CreateKpiTruongDto request, CancellationToken cancellationToken)
        {
            await _service.CreateKpiTruong(request);
            return;
        }
    }
}
