using D.ApplicationBase;
using D.Core.Domain.Dtos.Survey.Report;
using D.Core.Infrastructure.Services.Survey.Report.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.Survey
{
    public class GenerateReportSurvey : ICommandHandler<GenerateReportDto, bool>
    {
        private readonly IReportSurveyService _service;

        public GenerateReportSurvey(IReportSurveyService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(GenerateReportDto request, CancellationToken cancellationToken)
        {
            return await _service.GenerateReportAsync(request.SurveyId);
        }
    }
}
