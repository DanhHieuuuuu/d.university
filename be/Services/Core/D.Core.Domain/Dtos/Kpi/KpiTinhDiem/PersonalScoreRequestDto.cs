using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Kpi.KpiTinhDiem
{
    public class PersonalScoreRequestDto : IQuery<PersonalScoreDto>
    {
        public int IdNhanSu { get; set; }
        public string NamHoc { get; set; } = string.Empty;
    }
}
