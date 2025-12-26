using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Report
{
    public class QuestionStatDto
    {
        public int QuestionId { get; set; }
        public string Content { get; set; }
        public int Type { get; set; } // 1: Trắc nghiệm, 3: Tự luận
        public List<AnswerStatDto> Answers { get; set; } = new();
        public List<string> RecentTextResponses { get; set; } = new();
    }
}
