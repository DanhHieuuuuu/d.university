using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Submit
{
    public class StartSurveyResponseDto
    {
        public int SubmissionId { get; set; }
        public int SurveyId { get; set; }
        public string TenKhaoSat { get; set; }
        public DateTime ThoiGianBatDau { get; set; }

        public List<SurveyExamDto> Questions { get; set; } = new();
        public List<SavedAnswerDto> SavedAnswers { get; set; } = new();
    }
}
