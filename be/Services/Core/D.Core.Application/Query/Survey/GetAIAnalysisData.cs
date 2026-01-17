using D.Core.Domain.Dtos.Survey.AI;
using D.Core.Infrastructure.Services.Survey.Report.Abstracts;
using D.Core.Infrastructure.Services.Survey.Surveys.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.Survey
{
    public class GetAIAnalysisData : IRequestHandler<GetAIDataDto, SurveyAIDataDto>
    {
        private readonly IReportSurveyService _reportService;
        private readonly ISurveyService _surveyService;

        public GetAIAnalysisData(IReportSurveyService reportService, ISurveyService surveyService)
        {
            _reportService = reportService;
            _surveyService = surveyService;
        }

        public async Task<SurveyAIDataDto> Handle(GetAIDataDto request, CancellationToken cancellationToken)
        {
            return await _reportService.GetAIAnalysisDataAsync(request.ReportId);
        }
    }
}
