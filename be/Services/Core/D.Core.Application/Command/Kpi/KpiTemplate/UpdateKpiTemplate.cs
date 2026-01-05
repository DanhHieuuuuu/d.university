using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiTemplate;
using D.Core.Infrastructure.Services.Kpi.Abstracts;

namespace D.Core.Application.Command.Kpi.KpiTemplate
{
    public class UpdateKpiTemplate : ICommandHandler<UpdateKpiTemplateDto, KpiTemplateDto>
    {
        private readonly IKpiTemplateService _service;

        public UpdateKpiTemplate(IKpiTemplateService kpiTemplateService)
        {
            _service = kpiTemplateService;
        }

        public async Task<KpiTemplateDto> Handle(UpdateKpiTemplateDto request, CancellationToken cancellationToken)
        {
            return await _service.UpdateKpiTemplate(request);
        }
    }
}
