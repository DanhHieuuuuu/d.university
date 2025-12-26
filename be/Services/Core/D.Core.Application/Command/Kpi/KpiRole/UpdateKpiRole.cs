using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiRole;
using D.Core.Infrastructure.Services.Kpi.Abstracts;

namespace D.Core.Application.Command.Kpi.KpiRole
{
    public class UpdateKpiRole : ICommandHandler<UpdateKpiRoleDto>
    {
        private readonly IKpiRoleService _service;

        public UpdateKpiRole(IKpiRoleService kpiRoleService)
        {
            _service = kpiRoleService;
        }

        public async Task Handle(UpdateKpiRoleDto request, CancellationToken cancellationToken)
        {
            _service.UpdateKpiRole(request);
            return;
        }
    }
}
