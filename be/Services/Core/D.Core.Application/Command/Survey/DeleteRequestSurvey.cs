using D.ApplicationBase;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using D.Core.Domain.Dtos.Survey.Request;
using D.Core.Infrastructure.Services.Survey.Request.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.Survey
{
    public class DeleteRequestSurvey : ICommandHandler<DeleteRequestSurveyDto>
    {
        private readonly IRequestSurveyService _service;

        public DeleteRequestSurvey(IRequestSurveyService service)
        {
            _service = service;
        }

        public async Task Handle(DeleteRequestSurveyDto request, CancellationToken cancellationToken)
        {
             await _service.DeleteRequestSurvey(request);
        }
    }
}
