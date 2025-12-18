using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Request
{
     public class RequestQuestionAnswerDto
    {
        public string NoiDung { get; set; }
        public int Value { get; set; }
        public int ThuTu { get; set; }
        public bool IsCorrect { get; set; }
        public string MoTa { get; set; }
    }
}
