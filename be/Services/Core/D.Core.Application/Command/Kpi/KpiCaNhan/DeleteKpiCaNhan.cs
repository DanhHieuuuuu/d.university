using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmPhongBan;
using D.Core.Domain.Dtos.Kpi.KpiCaNhan;
using D.Core.Infrastructure.Services.Kpi.Abstracts;

namespace D.Core.Application.Command.Kpi.KpiCaNhan
{
    public class DeleteKpiCaNhan : ICommandHandler<DeleteKpiCaNhanDto>
    {
        private readonly IKpiCaNhanService _service;

        public DeleteKpiCaNhan(IKpiCaNhanService kpiCaNhanService)
        {
            _service = kpiCaNhanService;
        }

        public async Task Handle(DeleteKpiCaNhanDto request, CancellationToken cancellationToken)
        {
            _service.DeleteKpiCaNhan(request);
            return;
        }
    }
}
