using D.Core.Domain.Dtos.Survey.Submit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.Survey
{
    public class StartSurvey : IRequest<StartSurveyResponseDto>
    {
        public int SurveyId { get; set; }
        public StartSurvey(int id) => SurveyId = id;
    }
}
