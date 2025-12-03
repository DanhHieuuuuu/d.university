
using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Kpi.KpiRole
{
    public class CreateKpiRoleDto : ICommand
    {
        public int IdNhanSu { get; set; }
        public string Role { get; set; }
        public int IdDonVi { get; set; }
        public decimal TiLe { get; set; }
    }
}
