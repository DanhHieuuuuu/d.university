

namespace D.Core.Domain.Dtos.Kpi.KpiCaNhan
{
    public class KpiCaNhanSummaryDto
    {
        public decimal TongTuDanhGia { get; set; }
        public decimal TongCapTren { get; set; }
        public List<KpiCaNhanSummaryByLoaiDto> ByLoaiKpi { get; set; } = new();
    }

    public class KpiCaNhanSummaryByLoaiDto
    {
        public int LoaiKpi { get; set; }
        public decimal TuDanhGia { get; set; }
        public decimal CapTren { get; set; }
    }

}
