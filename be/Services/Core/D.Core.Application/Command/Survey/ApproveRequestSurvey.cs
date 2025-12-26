using D.ApplicationBase;
using D.Core.Domain.Dtos.Survey.Request;
using D.Core.Infrastructure.Services.Survey.Request.Abstracts;
using D.Core.Infrastructure.Services.Survey.Surveys.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace D.Core.Application.Command.Survey
{
    public class ApproveRequestSurvey : ICommandHandler<ApproveRequestDto>
    {
        private readonly IRequestSurveyService _requestService;
        private readonly ISurveyService _surveyService;

        public ApproveRequestSurvey(IRequestSurveyService requestService, ISurveyService surveyService)
        {
            _requestService = requestService;
            _surveyService = surveyService;
        }

        public async Task Handle(ApproveRequestDto request, CancellationToken cancellationToken)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                await _requestService.ApproveRequestAsync(request.Id);

                await _surveyService.CreateSurveyFromRequestAsync(request.Id);

                scope.Complete();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //public async Task Handle(ApproveRequestDto request, CancellationToken cancellationToken)
        //{
        //    // TẠM BỎ TRANSACTION SCOPE ĐỂ TEST LỖI GỐC
        //    // using var scope = new TransactionScope(...); <--- Comment dòng này lại

        //    try
        //    {
        //        // Bước 1: Duyệt
        //        await _requestService.ApproveRequestAsync(request.Id);

        //        // Bước 2: Tạo khảo sát
        //        await _surveyService.CreateSurveyFromRequestAsync(request.Id);

        //        // scope.Complete(); <--- Comment dòng này lại
        //    }
        //    catch (Exception ex)
        //    {
        //        // Bắt lỗi và in ra InnerException (lỗi sâu nhất)
        //        var msg = ex.Message;
        //        if (ex.InnerException != null)
        //        {
        //            msg += " | INNER: " + ex.InnerException.Message;
        //        }

        //        // Ném lỗi ra để Controller bắt được
        //        throw new Exception($"LỖI GỐC TÌM THẤY: {msg}");
        //    }
        //}
    }
}
