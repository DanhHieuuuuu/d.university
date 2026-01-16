using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Kpi.KpiDonVi
{
    public class GiaoKpiDonViDto : ICommand
    {
        public int IdKpiDonVi { get; set; }
        //public int IdDonVi { get; set; }
        //public string? NamHoc { get; set; }
        public List<GiaoKpiNhanSuDto> NhanSus { get; set; } = new();
    }

    public class GiaoKpiNhanSuDto
    {
        public int IdNhanSu { get; set; }
        public string? TrongSo { get; set; }
        
    }
}
