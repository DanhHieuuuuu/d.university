using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiTemplate;
using D.Core.Domain.Dtos.Kpi.KpiTruong;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.Kpi.KpiTemplate
{
    public class FindPagingKpiTemplate : IQueryHandler<FilterKpiTemplateDto, PageResultDto<KpiTemplateDto>>
    {
        private readonly IKpiTemplateService _service;

        public FindPagingKpiTemplate(IKpiTemplateService kpiTemplateService)
        {
            _service = kpiTemplateService;
        }

        public async Task<PageResultDto<KpiTemplateDto>> Handle(
            FilterKpiTemplateDto request,
            CancellationToken cancellationToken
        )
        {
            return _service.GetAllKpiTemplate(request);
        }
    }
}
