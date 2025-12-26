using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiCaNhan;
using D.Core.Domain.Dtos.Kpi.KpiDonVi;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.Kpi.KpiDonVi
{
    public class CreateKpiDonVi : ICommandHandler<CreateKpiDonViDto>
    {
        private readonly IKpiDonViService _service;

        public CreateKpiDonVi(IKpiDonViService kpiDonViService)
        {
            _service = kpiDonViService;
        }

        public async Task Handle(CreateKpiDonViDto request, CancellationToken cancellationToken)
        {
            _service.CreateKpiDonVi(request);
            return;
        }
    }
}
