using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Report
{
    public class AnswerStatDto
    {
        public string Label { get; set; }   // Nội dung đáp án
        public int Count { get; set; }      // Số người chọn
        public double Percent { get; set; } // Phần trăm
    }
}
