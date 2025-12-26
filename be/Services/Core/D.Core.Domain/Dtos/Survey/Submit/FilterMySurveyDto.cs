using D.Core.Domain.Dtos.Survey.Surveys;
using D.DomainBase.Common;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Submit
{
    public class FilterMySurveyDto : FilterBaseDto, IQuery<PageResultDto<SurveyResponseDto>>
    {
        public int? Status { get; set; }
    }
}
