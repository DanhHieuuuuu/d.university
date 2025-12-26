using D.ApplicationBase;
using D.Core.Domain.Dtos.Survey.Surveys;
using D.Core.Infrastructure.Services.Survey.Surveys.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.Survey
{
    public class GetSurveyDetail : IQueryHandler<GetSurveyDetailDto, SurveyDetailDto>
    {
        private readonly ISurveyService _service;

        public GetSurveyDetail(ISurveyService service)
        {
            _service = service;
        }

        public async Task<SurveyDetailDto> Handle(GetSurveyDetailDto request, CancellationToken cancellationToken)
        {
            return await _service.GetByIdSurvey(request.Id);
        }
    }
}
