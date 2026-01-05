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
    public class CancelSubmitRequestSurvey : ICommandHandler<CancelSubmitRequestDto>
    {
        private readonly IRequestSurveyService _service;

        public CancelSubmitRequestSurvey(IRequestSurveyService service)
        {
            _service = service;
        }

        public async Task Handle(CancelSubmitRequestDto request, CancellationToken cancellationToken)
        {
            await _service.CancelSubmitAsync(request.Id);
        }
    }
}
