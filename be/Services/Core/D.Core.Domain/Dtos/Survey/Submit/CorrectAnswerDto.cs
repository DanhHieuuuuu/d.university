using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Submit
{
    public class CorrectAnswerDto
    {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public int Value { get; set; }
    }
}
