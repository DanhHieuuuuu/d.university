
namespace D.Core.Domain.Dtos.Kpi.KpiDonVi
{
    public class NhanSuDaGiaoDto
    {
        public int Id { get; set; }
        public int IdNhanSu { get; set; }
        public string? HoTen { get; set; }
        public string? TrongSo { get; set; }
        public int? IdKpiDonVi { get; set; }
        public float? TyLeThamGia { get; set; }
    }
}
