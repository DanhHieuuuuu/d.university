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
    public class SubmitRequestSurvey : ICommandHandler<SubmitRequestDto>
    {
        private readonly IRequestService _service;

        public SubmitRequestSurvey(IRequestService service)
        {
            _service = service;
        }

        public async Task Handle(SubmitRequestDto request, CancellationToken cancellationToken)
        {
            await _service.SubmitRequestAsync(request.Id);
        }
    }
}
