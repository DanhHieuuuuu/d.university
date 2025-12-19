using D.ApplicationBase;
using D.Core.Domain.Dtos.Survey.Request;
using D.Core.Infrastructure.Services.Survey.Request.Abstracts;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.Survey
{
    public class GetRequestSurveyPaging : IQueryHandler<FilterSurveyRequestDto, PageResultDto<RequestSurveyResponseDto>>
    {
        private readonly IRequestService _service;

        public GetRequestSurveyPaging(IRequestService service)
        {
            _service = service;
        }

        public async Task<PageResultDto<RequestSurveyResponseDto>> Handle(FilterSurveyRequestDto request, CancellationToken cancellationToken)
        {
            return _service.Paging(request);
        }
    }
}
