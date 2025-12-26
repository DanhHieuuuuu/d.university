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
    public class GetMySurveyPaging : IRequestHandler<FilterMySurveyDto, PageResultDto<SurveyResponseDto>>
    {
        private readonly ISurveyService _service;

        public GetMySurveyPaging(ISurveyService service)
        {
            _service = service;
        }

        public async Task<PageResultDto<SurveyResponseDto>> Handle(FilterMySurveyDto request, CancellationToken cancellationToken)
        {
            return await _service.GetMySurveysAsync(request);
        }
    }
}
