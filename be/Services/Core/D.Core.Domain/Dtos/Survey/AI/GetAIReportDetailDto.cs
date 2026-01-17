using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.AI
{
    public class GetAIReportDetailDto : IRequest<List<AIReportDetailDto>>
    {
        public int ReportId { get; set; }

        public GetAIReportDetailDto(int reportId) => ReportId = reportId;
    }
}
