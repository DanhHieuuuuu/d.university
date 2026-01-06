using D.Core.Domain.Dtos.Survey.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Surveys
{
    public class SurveyDetailDto : SurveyResponseDto
    {       
        public int IdPhongBan { get; set; }
        public RequestSurveyDetailDto? SurveyRequest { get; set; }
    }
}
