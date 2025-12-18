using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Request
{
    public class RequestSurveyDetailDto : RequestSurveyRequestDto
    {
        public List<RequestSurveyTargetDto> Targets { get; set; } = new();
        public List<RequestSurveyQuestionDto> Questions { get; set; } = new();
        public List<RequestSurveyCriteriaDto> Criterias { get; set; } = new();
    }
}
