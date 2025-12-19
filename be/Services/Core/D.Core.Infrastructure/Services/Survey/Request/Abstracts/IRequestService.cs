using D.Core.Domain.Dtos.Survey.Request;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Services.Survey.Request.Abstracts
{
    public interface IRequestService
    {
        PageResultDto<RequestSurveyResponseDto> Paging(FilterSurveyRequestDto dto);
        Task<RequestSurveyDetailDto> GetByIdRequest(int id);

        Task<CreateRequestSurveyResponseDto> CreateRequestSurvey(CreateRequestSurveyRequestDto dto);
        Task<UpdateRequestSurveyResponseDto> UpdateRequestSurvey(UpdateRequestSurveyRequestDto dto);
        Task<bool> DeleteRequestSurvey(DeleteRequestSurveyDto dto);

        Task SubmitRequestAsync(int id);
        Task CancelSubmitAsync(int id);
    }
}
