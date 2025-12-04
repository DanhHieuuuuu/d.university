using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Kpi.KpiCaNhan
{
    public class CreateKpiCaNhanDto : ICommand
    {
        public string? KPI { get; set; }
        public int LoaiKPI { get; set; }
        public string? MucTieu { get; set; }
        public string? TrongSo { get; set; }
        public int IdNhanSu { get; set; }
        public string? NamHoc { get; set; }
    }
}
