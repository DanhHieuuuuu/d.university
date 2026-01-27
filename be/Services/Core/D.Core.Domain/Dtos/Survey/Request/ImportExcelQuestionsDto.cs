using D.Core.Domain.Dtos.Survey.Request;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace D.Core.Domain.Dtos.Survey.Request
{
    public class ImportExcelQuestionsDto : IRequest<List<RequestSurveyQuestionDto>>
    {
        public IFormFile File { get; set; }
    }
}
