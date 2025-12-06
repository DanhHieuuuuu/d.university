using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Kpi.KpiCaNhan
{
    public class UpdateKqThucTeKpiCaNhanDto
    {
        public int Id { get; set; }
        public decimal? KetQuaThucTe { get; set; }
    }
    public class UpdateKpiThucTeKpiCaNhanListDto : ICommand
    {
        public List<UpdateKqThucTeKpiCaNhanDto>? Items { get; set; }
    }
}
