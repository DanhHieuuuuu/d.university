using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiTemplate;
using D.Core.Infrastructure.Services.Kpi.Abstracts;

namespace D.Core.Application.Query.Kpi.KpiTemplate
{
    public class GetTemplateTypes : IQueryHandler<GetTemplateTypeRequestDto, List<TemplateTypeDto>>
    {
        private readonly IKpiTemplateService _service;

        public GetTemplateTypes(IKpiTemplateService kpiTemplateService)
        {
            _service = kpiTemplateService;
        }

        public async Task<List<TemplateTypeDto>> Handle(
            GetTemplateTypeRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return await _service.GetTemplateTypesAsync();
        }
    }
}
