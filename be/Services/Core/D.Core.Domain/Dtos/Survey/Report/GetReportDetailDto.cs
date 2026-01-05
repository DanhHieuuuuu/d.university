using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Report
{
    public class GetReportDetailDto : IRequest<SurveyReportDetailDto>
    {
        public int ReportId { get; set; }

        public GetReportDetailDto(int reportId) => ReportId = reportId;
    }
}
