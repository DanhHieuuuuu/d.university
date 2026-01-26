using D.ApplicationBase;
using D.Core.Domain.Dtos.Survey.Statistics;
using D.Core.Infrastructure.Services.Survey.Surveys.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.Survey
{
    public class GetSurveyStatistics : IQueryHandler<GetSurveyStatisticsDto, SurveyStatisticsDto>
    {
        private readonly ISurveyService _service;

        public GetSurveyStatistics(ISurveyService service)
        {
            _service = service;
        }

        public async Task<SurveyStatisticsDto> Handle(GetSurveyStatisticsDto request, CancellationToken cancellationToken)
        {
            return await _service.GetStatisticsAsync();
        }
    }
}
