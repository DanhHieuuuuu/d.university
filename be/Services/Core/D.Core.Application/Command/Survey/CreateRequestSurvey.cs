using D.ApplicationBase;
using D.Core.Domain.Dtos.Survey.Request;
using D.Core.Infrastructure.Services.Survey.Request.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.Survey
{
    public class CreateRequestSurvey : ICommandHandler<CreateRequestSurveyRequestDto, CreateRequestSurveyResponseDto>
    {
        private readonly IRequestSurveyService _service;

        public CreateRequestSurvey(IRequestSurveyService service)
        {
            _service = service;
        }

        public async Task<CreateRequestSurveyResponseDto> Handle(CreateRequestSurveyRequestDto request, CancellationToken cancellationToken)
        {
            return await _service.CreateRequestSurvey(request);
        }
    }
}
