using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Kpi.KpiTinhDiem
{
    public class UnitScoreRequestDto : IQuery<UnitScoreDto>
    {
        public int IdDonVi { get; set; }
        public string NamHoc { get; set; } = string.Empty;
    }
}
