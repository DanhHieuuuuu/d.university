
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
        public string? LoaiKetQua { get; set; }
        public decimal? KetQuaThucTe { get; set; }
        public decimal? DiemKpiCapTren { get; set; }
        public decimal? CapTrenDanhGia { get; set; }
        public decimal? DiemKpi { get; set; }
        public int? IsActive { get; set; }
        public string? GhiChu { get; set; }
        public string? CongThuc { get; set; }
    }
}
