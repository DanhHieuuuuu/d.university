using D.ApplicationBase;
using D.Core.Domain.Dtos.Survey.Request;
using D.Core.Infrastructure.Services.Survey.Request.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.Survey
{
    public class GetRequestSurveyDetail : IQueryHandler<GetRequestSurveyDetailDto, RequestSurveyDetailDto>
    {
        private readonly IRequestService _service;

        public GetRequestSurveyDetail(IRequestService service)
        {
            _service = service;
        }

        public async Task<RequestSurveyDetailDto> Handle(GetRequestSurveyDetailDto request, CancellationToken cancellationToken)
        {
            return await _service.GetByIdRequest(request.Id);
        }
    }
}
