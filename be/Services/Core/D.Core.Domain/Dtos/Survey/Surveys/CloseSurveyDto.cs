using D.DomainBase.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Surveys
{
    public class CloseSurveyDto : ICommand
    {
        public int Id { get; set; }
        public CloseSurveyDto(int id) => Id = id;
    }
}
