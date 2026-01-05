using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Report
{
    public class SurveyReportDetailDto
    {
        public int ReportId { get; set; }
        public string TenKhaoSat { get; set; }
        public int TotalParticipants { get; set; }
        public double AverageScore { get; set; }
        public DateTime LastGenerated { get; set; }

        public ReportStatisticsDto Statistics { get; set; }
        public List<SurveyRespondentDto> Respondents { get; set; }
    }
}
