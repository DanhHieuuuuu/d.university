using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Kpi.KpiDonVi
{
    public class UpdateKpiThucTeKpiDonViListDto : ICommand
    {
        public List<UpdateKqThucTeKpiDonViDto>? Items { get; set; }
    }
    public class UpdateKqThucTeKpiDonViDto
    {
        public int Id { get; set; }
        public decimal? KetQuaThucTe { get; set; }
        public decimal? DiemKpi { get; set; }
    }
}
