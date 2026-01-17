using D.Core.Domain.Dtos.Survey.Logging;
using D.Core.Domain.Dtos.Survey.Surveys;
using D.DomainBase.Common;
using D.DomainBase.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Log
{
    public class FilterSurveyLogDto : FilterBaseDto, IQuery<PageResultDto<LogSurveyResponseDto>>
    {
        public string? Keyword { get; set; }
        public string? LoaiHanhDong { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
    }
}
