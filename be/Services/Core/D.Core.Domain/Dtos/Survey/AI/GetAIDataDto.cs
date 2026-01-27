using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.AI
{
    public class GetAIDataDto : IRequest<SurveyAIDataDto>
    {
        public int ReportId { get; set; }

        public GetAIDataDto(int reportId) => ReportId = reportId;
    }
}
