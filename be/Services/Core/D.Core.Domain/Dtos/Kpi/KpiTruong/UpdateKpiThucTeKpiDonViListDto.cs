using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Kpi.KpiTruong
{
    public class UpdateKpiThucTeKpiTruongListDto : ICommand
    {
        public List<UpdateKqThucTeKpiTruongDto>? Items { get; set; }
    }
    public class UpdateKqThucTeKpiTruongDto
    {
        public int Id { get; set; }
        public decimal? KetQuaThucTe { get; set; }
    }
}
