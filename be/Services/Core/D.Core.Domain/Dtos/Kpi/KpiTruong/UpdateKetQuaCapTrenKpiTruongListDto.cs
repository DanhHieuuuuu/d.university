using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Kpi.KpiTruong
{
    public class UpdateKetQuaCapTrenKpiTruong
    {
        public int Id { get; set; }
        public decimal? KetQuaCapTren { get; set; }
        public decimal? DiemKpiCapTren { get; set; }
    }
    public class UpdateKetQuaCapTrenKpiTruongListDto : ICommand
    {
        public List<UpdateKetQuaCapTrenKpiTruong>? Items { get; set; }
    }
}
