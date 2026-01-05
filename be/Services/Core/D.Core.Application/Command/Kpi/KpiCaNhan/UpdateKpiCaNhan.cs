using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiCaNhan;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.Kpi.KpiCaNhan
{
    public class UpdateKpiCaNhan : ICommandHandler<UpdateKpiCaNhanDto>
    {
        private readonly IKpiCaNhanService _service;

        public UpdateKpiCaNhan(IKpiCaNhanService kpiCaNhanService)
        {
            _service = kpiCaNhanService;
        }

        public async Task Handle(UpdateKpiCaNhanDto request, CancellationToken cancellationToken)
        {
            _service.UpdateKpiCaNhan(request);
            return;
        }
    }
}
