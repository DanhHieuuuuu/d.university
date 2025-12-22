using D.DomainBase.Common;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Surveys
{
    public class FilterSurveyDto : FilterBaseDto, IQuery<PageResultDto<SurveyResponseDto>>
    {
        public string? Keyword { get; set; }
        public int? Status { get; set; }
        public int? IdPhongBan { get; set; }
    }
}
