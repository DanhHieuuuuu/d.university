using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Kpi.KpiDonVi
{
    public class UpdateKpiDonViDto : ICommand
    {
        public int Id { get; set; }
        public string Kpi { get; set; }
        public string MucTieu { get; set; }
        public string TrongSo { get; set; }
        public int IdDonVi { get; set; }
        public int LoaiKpi { get; set; }
        public string NamHoc { get; set; }
        public int? IdKpiTruong { get; set; }
        public int? IdCongThuc { get; set; }
        public string? CongThucTinh { get; set; }
        public string? LoaiKetQua { get; set; }
    }
}
