using D.Core.Domain.Dtos.Survey.AI;
using D.Core.Infrastructure.Services.Survey.Report.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.Survey
{
    public class GetAIReportDetail : IRequestHandler<GetAIReportDetailDto, List<AIReportDetailDto>>
    {
        private readonly IReportSurveyService _service;

        public GetAIReportDetail(IReportSurveyService service)
        {
            _service = service;
        }

        public async Task<List<AIReportDetailDto>> Handle(GetAIReportDetailDto request, CancellationToken cancellationToken)
        {
            return await _service.GetAIResponsesByReportIdAsync(request.ReportId);
        }
    }
}
