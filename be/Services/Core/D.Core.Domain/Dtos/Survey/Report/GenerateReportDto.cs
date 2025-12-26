using D.DomainBase.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Report
{
    public class GenerateReportDto : ICommand<bool>
    {
        public int SurveyId { get; set; }

        public GenerateReportDto(int id) => SurveyId = id;
    }
}
