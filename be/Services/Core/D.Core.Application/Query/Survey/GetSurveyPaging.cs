using D.ApplicationBase;
using D.Core.Domain.Dtos.Survey.Surveys;
using D.Core.Infrastructure.Services.Survey.Surveys.Abstracts;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.Survey
{
    public class GetSurveyPaging : IQueryHandler<FilterSurveyDto, PageResultDto<SurveyResponseDto>>
    {
        private readonly ISurveyService _service;

        public GetSurveyPaging(ISurveyService service)
        {
            _service = service;
        }

        public async Task<PageResultDto<SurveyResponseDto>> Handle(FilterSurveyDto request, CancellationToken cancellationToken)
        {
            return _service.Paging(request);
        }
    }
}
