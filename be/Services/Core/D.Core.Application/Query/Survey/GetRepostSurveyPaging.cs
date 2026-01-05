using D.ApplicationBase;
using D.Core.Domain.Dtos.Survey.Report;
using D.Core.Domain.Dtos.Survey.Request;
using D.Core.Infrastructure.Services.Survey.Report.Abstracts;
using D.Core.Infrastructure.Services.Survey.Request.Abstracts;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.Survey
{
    public class GetRepostSurveyPaging : IQueryHandler<FilterReportSurveyDto, PageResultDto<SurveyReportResponseDto>>
    {
        private readonly IReportSurveyService _service;

        public GetRepostSurveyPaging(IReportSurveyService service)
        {
            _service = service;
        }

        public async Task<PageResultDto<SurveyReportResponseDto>> Handle(FilterReportSurveyDto request, CancellationToken cancellationToken)
        {
            return await _service.GetReportsPagingAsync(request);
        }
    }
}
