using D.DomainBase.Common;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Report
{
    public class FilterReportSurveyDto : FilterBaseDto, IQuery<PageResultDto<SurveyReportResponseDto>>
    {
        public string? Keyword { get; set; }
    }
}
