using D.ApplicationBase;
using D.Core.Domain.Dtos.Survey.Submit;
using D.Core.Infrastructure.Services.Survey.Surveys.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.Survey
{
    public class SubmitSurvey : ICommandHandler<SubmitSurveyDto, SurveyResultDto>
    {
        private readonly ISurveyService _service;

        public SubmitSurvey(ISurveyService service)
        {
            _service = service;
        }

        public async Task<SurveyResultDto> Handle(SubmitSurveyDto request, CancellationToken cancellationToken)
        {
            return await _service.SubmitSurveyAsync(request);
        }
    }
}
