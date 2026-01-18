using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Kpi.KpiTinhDiem
{
    namespace D.Core.Domain.Dtos.Kpi.KpiTinhDiem
    {
        public class GetKpiScoreBoardDto : IQuery<KpiDashboardResponse>
        {
            public string NamHoc { get; set; }
            public int? ViewUnitId { get; set; }
        }
        public class KpiDashboardResponse
        {
            public string ViewMode { get; set; }
            public PersonalScoreDto MyScore { get; set; }
            public SchoolScoreDto SchoolScore { get; set; }
            public List<UnitScoreDto> AllUnits { get; set; }
            public UnitScoreDto CurrentUnitScore { get; set; }
            public List<PersonalScoreDto> StaffScores { get; set; }
        }
    }
}
