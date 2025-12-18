using D.DomainBase.Common;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Request
{
    public class FilterSurveyRequestDto : FilterBaseDto, IQuery<PageResultDto<RequestSurveyRequestDto>>
    {
        public string Keyword { get; set; }
        public int? TrangThai { get; set; }
    }
}
