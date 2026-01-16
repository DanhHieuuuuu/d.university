

namespace D.Core.Domain.Dtos.Kpi.KpiDonVi
{
    public class KpiDonViSummaryDto
    {
        public decimal TongTuDanhGia { get; set; }
        public decimal TongCapTren { get; set; }
        public List<KpiDonViSummaryByLoaiDto> ByLoaiKpi { get; set; } = new();
    }

    public class KpiDonViSummaryByLoaiDto
    {
        public int LoaiKpi { get; set; }
        public decimal TuDanhGia { get; set; }
        public decimal CapTren { get; set; }
    }

}
