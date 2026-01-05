using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiTruong;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.Kpi.KpiTruong
{
    public class DeleteKpiTruong : ICommandHandler<DeleteKpiTruongDto>
    {
        private readonly IKpiTruongService _service;

        public DeleteKpiTruong(IKpiTruongService kpiTruongService)
        {
            _service = kpiTruongService;
        }

        public async Task Handle(DeleteKpiTruongDto request, CancellationToken cancellationToken)
        {
            await _service.DeleteKpi(request);
        }
    }
}
