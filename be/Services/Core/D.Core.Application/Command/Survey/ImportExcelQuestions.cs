using D.Core.Domain.Dtos.Survey.Request;
using D.Core.Infrastructure.Services.Survey.Request.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace D.Core.Application.Command.Survey
{
    public class ImportExcelQuestions : IRequestHandler<ImportExcelQuestionsDto, List<RequestSurveyQuestionDto>>
    {
        private readonly IRequestSurveyService _requestSurveyService;

        public ImportExcelQuestions(IRequestSurveyService requestSurveyService)
        {
            _requestSurveyService = requestSurveyService;
        }

        public async Task<List<RequestSurveyQuestionDto>> Handle(ImportExcelQuestionsDto request, CancellationToken cancellationToken)
        {
            if (request.File == null || request.File.Length == 0)
            {
                throw new Exception("File không hợp lệ");
            }

            // Validate file extension
            var extension = System.IO.Path.GetExtension(request.File.FileName).ToLower();
            if (extension != ".xlsx" && extension != ".xls")
            {
                throw new Exception("Chỉ chấp nhận file Excel (.xlsx, .xls)");
            }

            // Validate file size (max 5MB)
            if (request.File.Length > 5 * 1024 * 1024)
            {
                throw new Exception("File vượt quá kích thước cho phép (5MB)");
            }

            using (var stream = request.File.OpenReadStream())
            {
                var questions = _requestSurveyService.ReadExcel(stream);
                return await Task.FromResult(questions);
            }
        }
    }
}
