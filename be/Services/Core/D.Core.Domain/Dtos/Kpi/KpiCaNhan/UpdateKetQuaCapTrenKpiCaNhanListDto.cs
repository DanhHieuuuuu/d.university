using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Kpi.KpiCaNhan
{
    public class UpdateKetQuaCapTrenKpiCaNhan
    {
        public int Id { get; set; }
        public decimal? KetQuaCapTren { get; set; }
        public decimal? DiemKpiCapTren { get; set; }
    }
    public class UpdateKetQuaCapTrenKpiCaNhanListDto : ICommand
    {
        public List<UpdateKetQuaCapTrenKpiCaNhan>? Items { get; set; }
    }
}