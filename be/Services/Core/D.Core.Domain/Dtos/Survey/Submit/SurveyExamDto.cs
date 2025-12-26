using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Submit
{
    public class SurveyExamDto
    {
        public int Id { get; set; }
        public string NoiDung { get; set; }
        public int LoaiCauHoi { get; set; }
        public List<AnswerExamDto> Answers { get; set; }
    }
}
