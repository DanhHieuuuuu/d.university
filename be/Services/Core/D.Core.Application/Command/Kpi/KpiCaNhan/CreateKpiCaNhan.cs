using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiCaNhan;
using D.Core.Domain.Dtos.Kpi.KpiRole;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
