using D.Core.Domain.Dtos.Survey.Surveys;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Services.Survey.Surveys.Abstracts
{
    public interface ISurveyService
    {
        PageResultDto<SurveyResponseDto> Paging(FilterSurveyDto dto);
        Task<SurveyDetailDto> GetByIdSurvey(int id);
        Task CreateSurveyFromRequestAsync(int requestId);

        Task OpenSurveyAsync(int id);
        Task CloseSurveyAsync(int id);
    }
}
