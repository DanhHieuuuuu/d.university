using D.Core.Domain.Entities.Kpi;
using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Kpi.KpiTruong
{
    public class UpdateKpiTruongDto : ICommand
    {
        public int Id { get; set; }
        public string? LinhVuc { get; set; }
        public string? ChienLuoc { get; set; }
        public string? Kpi { get; set; }
        public string? MucTieu { get; set; }
        public string? TrongSo { get; set; }
        public int LoaiKpi { get; set; }
        public string? NamHoc { get; set; }
        public decimal? KetQuaThucTe { get; set; }
        public int? IdCongThuc { get; set; }
        public string? CongThucTinh { get; set; }
        public string? LoaiKetQua { get; set; }
    }
}
