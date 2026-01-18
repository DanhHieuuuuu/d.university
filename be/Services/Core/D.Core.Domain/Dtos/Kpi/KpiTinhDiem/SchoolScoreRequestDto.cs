using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Kpi.KpiTinhDiem
{
    public class SchoolScoreRequestDto : IQuery<SchoolScoreDto>
    {
        public string NamHoc { get; set; } = string.Empty;
    }
}
