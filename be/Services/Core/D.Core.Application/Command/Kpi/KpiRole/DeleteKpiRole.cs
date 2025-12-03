using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmChucVu;
using D.Core.Domain.Dtos.Kpi.KpiRole;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.Kpi.KpiRole
{
    public class DeleteKpiRole : ICommandHandler<DeleteKpiRoleDto>
    {
        private readonly IKpiRoleService _service;

        public DeleteKpiRole(IKpiRoleService kpiRoleService)
        {
            _service = kpiRoleService;
        }

        public async Task Handle(DeleteKpiRoleDto request, CancellationToken cancellationToken)
        {
            _service.Delete(request);
            return;
        }
    }
}
