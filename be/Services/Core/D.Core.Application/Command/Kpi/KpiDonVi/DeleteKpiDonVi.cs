using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiDonVi;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.Kpi.KpiDonVi
{
    public class DeleteKpiDonVi : ICommandHandler<DeleteKpiDonViDto>
    {
        private readonly IKpiDonViService _service;

        public DeleteKpiDonVi(IKpiDonViService kpiDonViService)
        {
            _service = kpiDonViService;
        }

        public async Task Handle(DeleteKpiDonViDto request, CancellationToken cancellationToken)
        {
            _service.DeleteKpiDonVi(request);
            return;
        }
    }
}
