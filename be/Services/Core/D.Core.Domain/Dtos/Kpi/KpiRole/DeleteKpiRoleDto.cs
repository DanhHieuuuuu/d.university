using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Kpi.KpiRole
{
    public class DeleteKpiRoleDto : ICommand
    {
        public required List<int> Ids { get; set; }
    }
}
