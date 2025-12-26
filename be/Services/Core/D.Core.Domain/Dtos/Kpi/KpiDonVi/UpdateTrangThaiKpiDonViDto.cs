using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Kpi.KpiDonVi
{
    public class UpdateTrangThaiKpiDonViDto : ICommand
    {
        public List<int> Ids { get; set; } = new List<int>();
        public int TrangThai { get; set; }
        public string? Note { get; set; }
    }
}
