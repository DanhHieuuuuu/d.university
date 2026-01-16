using D.Core.Domain.Dtos.Survey.Report;
using D.Core.Domain.Dtos.Survey.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.AI
{
    public class SurveyAIDataDto
    {
        public int ReportId { get; set; }
        public string TenKhaoSat { get; set; }
        public int TotalParticipants { get; set; }
        public double AverageScore { get; set; }
     
        public ReportStatisticsDto Statistics { get; set; }
        public List<RequestSurveyTargetDto> Targets { get; set; }
        public List<RequestSurveyQuestionDto> Questions { get; set; }
        public List<RequestSurveyCriteriaDto> Criteria { get; set; }
        
        
    }
}
