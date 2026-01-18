

namespace D.Core.Domain.Dtos.Kpi.KpiTinhDiem
{
    public class UnitScoreDto
    {
        public int IdDonVi { get; set; }
        public string TenDonVi { get; set; }
        public decimal DiemKpiDonVi { get; set; } 
        public string XepLoaiDonVi { get; set; }
        public bool IsFinalized { get; set; }
    }
}
