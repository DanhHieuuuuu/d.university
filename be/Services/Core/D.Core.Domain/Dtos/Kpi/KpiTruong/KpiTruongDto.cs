
using D.Core.Domain.Entities.Kpi;

namespace D.Core.Domain.Dtos.Kpi.KpiTruong
{
    public class KpiTruongDto
    {
        public int Id { get; set; }
        public string? LinhVuc { get; set; }
        public string? ChienLuoc { get; set; }
        public string? Kpi { get; set; }
        public string? MucTieu { get; set; }
        public string? TrongSo { get; set; }
        public int? LoaiKpi { get; set; }
        public string? NamHoc { get; set; }
        public int? TrangThai { get; set; }
        public LoaiCongThuc? LoaiCongThuc { get; set; }
        public decimal? KetQuaThucTe { get; set; }
        public int? IsActive { get; set; }
    }
}
