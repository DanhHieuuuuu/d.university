using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Kpi.KpiRole
{
    public class UpdateKpiRoleDto : ICommand
    {
        public int Id { get; set; }
        public int IdNhanSu { get; set; }
        public string Role { get; set; }
        public int IdDonVi { get; set; }
        public decimal TiLe { get; set; }
    }
}
