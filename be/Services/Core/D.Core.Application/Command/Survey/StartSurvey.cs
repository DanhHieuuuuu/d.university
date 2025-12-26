using D.ApplicationBase;
using D.Core.Domain.Dtos.Survey.Submit;
using D.Core.Infrastructure.Services.Survey.Surveys.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.Survey
{
    public class StartSurvey : ICommandHandler<StartSurveyDto, StartSurveyResponseDto>
    {
        private readonly ISurveyService _service;

        public StartSurvey(ISurveyService service)
        {
            _service = service;
        }

        public async Task<StartSurveyResponseDto> Handle(StartSurveyDto request, CancellationToken cancellationToken)
        {
            return await _service.StartSurveyAsync(request.SurveyId);
        }
    }
}
