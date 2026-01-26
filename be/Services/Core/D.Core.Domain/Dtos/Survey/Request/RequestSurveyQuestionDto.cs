using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Request
{
    public class RequestSurveyQuestionDto
    {
        public int IdCauHoi { get; set; }
        public string MaCauHoi { get; set; }
        public string NoiDung { get; set; }
        public int LoaiCauHoi { get; set; }
        public bool BatBuoc { get; set; }
        public int ThuTu { get; set; }
        public List<RequestQuestionAnswerDto> Answers { get; set; } = new();
    }
}
