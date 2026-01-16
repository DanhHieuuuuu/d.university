using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Submit
{
    public class SavedAnswerDto
    {
        public int QuestionId { get; set; }
        public int? SelectedAnswerId { get; set; } // For single choice (type 1)
        public List<int>? SelectedAnswerIds { get; set; } // For multiple choice (type 2)
        public string? TextResponse { get; set; } // For essay (type 3)
    }
}
