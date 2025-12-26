using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Report
{
    public class SurveyRespondentDto
    {
        public int SubmissionId { get; set; }
        public string UserCode { get; set; }  // Mã NV/SV
        public string FullName { get; set; }
        public DateTime? SubmitTime { get; set; }
        public double TotalScore { get; set; }
    }
}
