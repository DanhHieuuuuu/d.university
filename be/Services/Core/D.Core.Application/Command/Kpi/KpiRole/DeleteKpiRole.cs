using D.ApplicationBase;
using D.Core.Domain.Dtos.Kpi.KpiRole;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using MediatR;
using Newtonsoft.Json.Linq;

namespace D.Core.Application.Command.Kpi.KpiRole
{
    public class DeleteKpiRole : ICommandHandler<DeleteKpiRoleDto>
    {
        private readonly IKpiRoleService _service;

        public DeleteKpiRole(IKpiRoleService kpiRoleService)
        {
            _service = kpiRoleService;
        }

        public async Task<Unit> Handle(DeleteKpiRoleDto request, CancellationToken cancellationToken)
        {
            _service.Delete(request);
            return Unit.Value;
        }

        Task IRequestHandler<DeleteKpiRoleDto>.Handle(DeleteKpiRoleDto request, CancellationToken cancellationToken)
        {
            return Handle(request, cancellationToken);
        }
    }
}
