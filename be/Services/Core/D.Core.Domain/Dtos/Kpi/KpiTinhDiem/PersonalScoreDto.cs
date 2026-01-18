namespace D.Core.Domain.Dtos.Kpi.KpiTinhDiem
{
    public class PersonalScoreDto
    {
        public int IdNhanSu { get; set; }
        public string HoTen { get; set; }
        public string ChucVuChinh { get; set; } 
        public decimal DiemTongKet { get; set; }
        public string XepLoai { get; set; }   
        public bool IsFinalized { get; set; } 
    }
}
