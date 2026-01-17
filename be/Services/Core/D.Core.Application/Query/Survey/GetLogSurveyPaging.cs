using D.Core.Domain.Dtos.Survey.Log;
using D.Core.Domain.Dtos.Survey.Logging;
using D.Core.Domain.Dtos.Survey.Submit;
using D.Core.Domain.Dtos.Survey.Surveys;
using D.Core.Infrastructure.Services.Survey.Surveys.Abstracts;
using D.DomainBase.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.Survey
{
    public class GetLogSurveyPaging : IRequestHandler<FilterSurveyLogDto, PageResultDto<LogSurveyResponseDto>>
    {
        private readonly ISurveyService _service;

        public GetLogSurveyPaging(ISurveyService service)
        {
            _service = service;
        }

        public async Task<PageResultDto<LogSurveyResponseDto>> Handle(FilterSurveyLogDto request, CancellationToken cancellationToken)
        {
            return _service.LogPaging(request);
        }
    }
}
