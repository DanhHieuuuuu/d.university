using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiTemplate;
using D.Core.Infrastructure.Services.Kpi.Abstracts;


namespace D.Core.Application.Command.Kpi.KpiTemplate
{
    public class CreateKpiTemplate : ICommandHandler<CreateKpiTemplateDto, KpiTemplateDto>
    {
        private readonly IKpiTemplateService _service;

        public CreateKpiTemplate(IKpiTemplateService kpiTemplateService)
        {
            _service = kpiTemplateService;
        }

        public async Task<KpiTemplateDto> Handle(CreateKpiTemplateDto request, CancellationToken cancellationToken)
        {
            return await _service.CreateKpiTemplate(request);
        }
    }
}
