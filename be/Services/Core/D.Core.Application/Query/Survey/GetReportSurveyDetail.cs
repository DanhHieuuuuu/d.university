using D.Core.Domain.Dtos.Survey.Report;
using D.Core.Infrastructure.Services.Survey.Report.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.Survey
{
    public class GetReportSurveyDetail : IRequestHandler<GetReportDetailDto, SurveyReportDetailDto>
    {
        private readonly IReportSurveyService _service;

        public GetReportSurveyDetail(IReportSurveyService service)
        {
            _service = service;
        }

        public async Task<SurveyReportDetailDto> Handle(GetReportDetailDto request, CancellationToken cancellationToken)
        {
            return await _service.GetReportDetailAsync(request.ReportId);
        }
    }
}
