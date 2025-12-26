using D.DomainBase.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Submit
{
    public class SubmitSurveyDto : SubmitSurveyRequestDto, ICommand<SurveyResultDto>
    {
    }
}
