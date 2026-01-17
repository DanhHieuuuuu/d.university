using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.AI
{
    public class AnalyzeSurveyWithAIDto : IRequest<bool>
    {
        public int ReportId { get; set; }
    }
}
