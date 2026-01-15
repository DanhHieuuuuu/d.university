

namespace D.Core.Domain.Dtos.Kpi.KpiTruong
{
    public class KpiTruongSummaryDto
    {
        public decimal TongTuDanhGia { get; set; }
        public decimal TongCapTren { get; set; }
        public List<KpiTruongSummaryByLoaiDto> ByLoaiKpi { get; set; } = new();
    }

    public class KpiTruongSummaryByLoaiDto
    {
        public int LoaiKpi { get; set; }
        public decimal TuDanhGia { get; set; }
        public decimal CapTren { get; set; }
    }

}
