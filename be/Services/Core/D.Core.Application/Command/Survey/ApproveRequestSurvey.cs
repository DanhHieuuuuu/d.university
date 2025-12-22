using D.ApplicationBase;
using D.Core.Domain.Dtos.Survey.Request;
using D.Core.Infrastructure.Services.Survey.Request.Abstracts;
using D.Core.Infrastructure.Services.Survey.Surveys.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace D.Core.Application.Command.Survey
{
    public class ApproveRequestSurvey : ICommandHandler<ApproveRequestDto>
    {
        private readonly IRequestSurveyService _requestService;
        private readonly ISurveyService _surveyService;

        public ApproveRequestSurvey(IRequestSurveyService requestService, ISurveyService surveyService)
        {
            _requestService = requestService;
            _surveyService = surveyService;
        }

        public async Task Handle(ApproveRequestDto request, CancellationToken cancellationToken)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                await _requestService.ApproveRequestAsync(request.Id);

                await _surveyService.CreateSurveyFromRequestAsync(request.Id);

                scope.Complete();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
