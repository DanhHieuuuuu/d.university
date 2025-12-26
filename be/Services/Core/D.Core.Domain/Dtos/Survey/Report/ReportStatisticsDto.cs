using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Report
{
    public class ReportStatisticsDto
    {
        public List<QuestionStatDto> Questions { get; set; } = new();
    }
}
