using D.Core.Domain.Dtos.Survey.Report;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Services.Survey.Report.Abstracts
{
    public interface IReportSurveyService
    {
        Task<bool> GenerateReportAsync(int surveyId);
        Task<PageResultDto<SurveyReportResponseDto>> GetReportsPagingAsync(FilterReportSurveyDto dto);
        Task<SurveyReportDetailDto> GetReportDetailAsync(int reportId);
    }
}
