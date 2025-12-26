using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Submit
{
    public class SurveyResultDto
    {
        public int SubmissionId { get; set; }
        public double TotalScore { get; set; }
        public int TotalCorrect { get; set; }
        public int TotalQuestions { get; set; }
        public DateTime SubmitTime { get; set; }
    }
}
