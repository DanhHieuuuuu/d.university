using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiTemplate;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.Kpi.KpiTemplate
{
    public class SyncKpiTemplate : ICommandHandler<SyncKpiTemplateRequestDto, List<SyncKpiTemplateResponseDto>>
    {
        private readonly IKpiTemplateService _service;

        public SyncKpiTemplate(IKpiTemplateService kpiTemplateService)
        {
            _service = kpiTemplateService;
        }

        public async Task<List<SyncKpiTemplateResponseDto>> Handle(SyncKpiTemplateRequestDto request, CancellationToken cancellationToken)
        {
           return await _service.SyncKpiTemplate(request);
        }
    }
}
