using D.Core.Domain.Dtos.Survey.Report;
using D.Core.Domain.Dtos.Survey.AI;
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
        Task<SurveyAIDataDto> GetAIAnalysisDataAsync(int reportId);
        Task<bool> SaveAIResponseAsync(int reportId, List<AIReportDto> responses);
        Task<List<AIReportDetailDto>> GetAIResponsesByReportIdAsync(int reportId);
    }
}
