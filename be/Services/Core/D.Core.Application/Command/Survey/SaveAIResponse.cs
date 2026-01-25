using D.Core.Domain.Dtos.Survey.AI;
using D.Core.Infrastructure.Services.Survey.Report.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.Survey
{
    public class SaveAIResponse : IRequestHandler<SaveAIResponseDto, bool>
    {
        private readonly IReportSurveyService _service;

        public SaveAIResponse(IReportSurveyService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(SaveAIResponseDto request, CancellationToken cancellationToken)
        {
            return await _service.SaveAIResponseAsync(request.ReportId, request.Responses);
        }
    }
}
