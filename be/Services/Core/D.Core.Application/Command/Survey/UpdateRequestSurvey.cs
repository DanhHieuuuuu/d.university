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
    public class UpdateRequestSurvey : ICommandHandler<UpdateRequestSurveyRequestDto, UpdateRequestSurveyResponseDto>
    {
        private readonly IRequestService _service;

        public UpdateRequestSurvey(IRequestService service)
        {
            _service = service;
        }

        public async Task<UpdateRequestSurveyResponseDto> Handle(UpdateRequestSurveyRequestDto request, CancellationToken cancellationToken)
        {
            return await _service.UpdateRequestSurvey(request);
        }
    }
}
