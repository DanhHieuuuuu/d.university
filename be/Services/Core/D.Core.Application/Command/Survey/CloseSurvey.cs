using D.ApplicationBase;
using D.Core.Domain.Dtos.Survey.Surveys;
using D.Core.Infrastructure.Services.Survey.Surveys.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.Survey
{
    public class CloseSurvey : ICommandHandler<CloseSurveyDto>
    {
        private readonly ISurveyService _service;
        public CloseSurvey(ISurveyService service)
        {
            _service = service;
        }

        public async Task Handle(CloseSurveyDto request, CancellationToken cancellationToken)
        {
            await _service.CloseSurveyAsync(request.Id);
        }
    }
}
