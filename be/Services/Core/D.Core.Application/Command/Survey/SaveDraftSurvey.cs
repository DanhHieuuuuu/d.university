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
    public class SaveDraft : ICommandHandler<SaveDraftDto, bool>
    {
        private readonly ISurveyService _service;

        public SaveDraft(ISurveyService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(SaveDraftDto request, CancellationToken cancellationToken)
        {
            await _service.SaveDraftAsync(request);
            return true;
        }
    }
}
