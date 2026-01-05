using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiCaNhan;
using D.Core.Infrastructure.Services.Kpi.Abstracts;

namespace D.Core.Application.Command.Kpi.KpiCaNhan
{
    public class CreateKpiCaNhan : ICommandHandler<CreateKpiCaNhanDto>
    {
        private readonly IKpiCaNhanService _service;

        public CreateKpiCaNhan(IKpiCaNhanService kpiCaNhanService)
        {
            _service = kpiCaNhanService;
        }

        public async Task Handle(CreateKpiCaNhanDto request, CancellationToken cancellationToken)
        {
            _service.CreateKpiCaNhan(request);
            return;
        }
    }
}
