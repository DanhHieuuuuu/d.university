using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Statistics
{
    public class SurveyStatisticsDto
    {
        public SurveyRequestStatsDto SurveyRequests { get; set; }
        public SurveyStatsDto Surveys { get; set; }
    }

    public class SurveyRequestStatsDto
    {
        public int Total { get; set; }
        public List<StatusCountDto> ByStatus { get; set; }
    }

    public class SurveyStatsDto
    {
        public int Total { get; set; }
        public List<StatusCountDto> ByStatus { get; set; }
    }

    public class StatusCountDto
    {
        public int Status { get; set; }
        public string StatusName { get; set; }
        public int Count { get; set; }
    }
}
