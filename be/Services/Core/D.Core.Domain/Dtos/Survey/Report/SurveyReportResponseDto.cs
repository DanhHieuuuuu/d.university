using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Report
{
    public class SurveyReportResponseDto
    {
        public int ReportId { get; set; }
        public int SurveyId { get; set; }
        public string TenKhaoSat { get; set; }
        public int TotalParticipants { get; set; }
        public double AverageScore { get; set; }
        public DateTime GeneratedAt { get; set; }
    }
}
