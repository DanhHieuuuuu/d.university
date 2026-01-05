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
    public class UpdateTrangThai : ICommandHandler<UpdateTrangThaiKpiDto>
    {
        private readonly IKpiCaNhanService _service;

        public UpdateTrangThai(IKpiCaNhanService kpiCaNhanService)
        {
            _service = kpiCaNhanService;
        }

        public async Task Handle(UpdateTrangThaiKpiDto request, CancellationToken cancellationToken)
        {
            _service.UpdateTrangThaiKpiCaNhan(request);
            return;
        }
    }
}
