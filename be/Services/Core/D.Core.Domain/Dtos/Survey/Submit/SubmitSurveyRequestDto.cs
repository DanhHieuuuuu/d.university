using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Submit
{
    public class SubmitSurveyRequestDto
    {
        public int SubmissionId { get; set; }
        public List<SavedAnswerDto> Answers { get; set; } = new();
    }
}
