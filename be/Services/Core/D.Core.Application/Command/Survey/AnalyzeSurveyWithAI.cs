using D.Core.Domain.Dtos.Survey.AI;
using D.Core.Infrastructure.Services.Survey.AI.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.Survey
{
    public class AnalyzeSurveyWithAI : IRequestHandler<AnalyzeSurveyWithAIDto, bool>
    {
        private readonly IAISurveyService _service;

        public AnalyzeSurveyWithAI(IAISurveyService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(AnalyzeSurveyWithAIDto request, CancellationToken cancellationToken)
        {
            return await _service.AnalyzeSurveyWithAIAsync(request.ReportId);
        }
    }
}
