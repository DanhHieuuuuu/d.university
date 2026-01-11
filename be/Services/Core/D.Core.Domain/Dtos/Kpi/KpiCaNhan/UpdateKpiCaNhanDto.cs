using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Kpi.KpiCaNhan
{
    public class UpdateKpiCaNhanDto : ICommand
    {
        public int Id { get; set; }
        public string? KPI { get; set; }
        public string? MucTieu { get; set; }
        public string? TrongSo { get; set; }
        public int IdNhanSu { get; set; }
        public string? NamHoc { get; set; }
        public int LoaiKPI { get; set; }
        public int? IdCongThuc { get; set; }
        public string? CongThucTinh { get; set; }
        public string? LoaiKetQua { get; set; }
        public string? Role { get; set; }
    }
}
