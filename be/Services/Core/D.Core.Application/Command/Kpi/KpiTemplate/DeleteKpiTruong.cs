using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiTemplate;
using D.Core.Domain.Dtos.Kpi.KpiTruong;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.Kpi.KpiTemplate
{
    public class DeleteKpiTemplate : ICommandHandler<DeleteKpiTemplateDto>
    {
        private readonly IKpiTemplateService _service;

        public DeleteKpiTemplate(IKpiTemplateService kpiTemplateService)
        {
            _service = kpiTemplateService;
        }

        public async Task Handle(DeleteKpiTemplateDto request, CancellationToken cancellationToken)
        {
            await _service.DeleteKpiTemplate(request);
        }
    }
}
