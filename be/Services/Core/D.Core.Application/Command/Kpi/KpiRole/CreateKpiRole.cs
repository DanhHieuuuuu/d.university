using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiRole;
using D.Core.Infrastructure.Services.Kpi.Abstracts;

namespace D.Core.Application.Command.Kpi.KpiRole
{
    public class CreateKpiRole : ICommandHandler<CreateKpiRoleDto>
    {
        private readonly IKpiRoleService _service;

        public CreateKpiRole(IKpiRoleService kpiRoleService)
        {
            _service = kpiRoleService;
        }

        public async Task Handle(CreateKpiRoleDto request, CancellationToken cancellationToken)
        {
            _service.CreateKpiRole(request);
            return;
        }
    }
}
