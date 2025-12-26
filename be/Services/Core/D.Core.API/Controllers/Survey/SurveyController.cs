using D.ControllerBase;
using D.Core.Application.Command.Survey;
using D.Core.Domain.Dtos.Survey.Report;
using D.Core.Domain.Dtos.Survey.Request;
using D.Core.Domain.Dtos.Survey.Submit;
using D.Core.Domain.Dtos.Survey.Surveys;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.API.Controllers.Survey
{
    [Route("/api/survey")]
    [ApiController]
    public class SurveyController : APIControllerBase
    {
        private readonly IMediator _mediator;

        public SurveyController(IMediator mediator)
            : base(mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Lấy danh sách yêu cầu khảo sát (Paging)
        /// </summary>
        /// /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("paging-request")]
        public async Task<ResponseAPI> PagingRequest([FromQuery] FilterSurveyRequestDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new ResponseAPI(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Lấy chi tiết yêu cầu khảo sát theo Id
        /// </summary>
        /// /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("get-request-by-id/{id}")]
        public async Task<ResponseAPI> GetRequestById([FromRoute] int id)
        {
            try
            {
                var query = new GetRequestSurveyDetailDto(id);
                var result = await _mediator.Send(query);
                return new ResponseAPI(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Tạo mới yêu cầu khảo sát
        /// </summary>
        /// /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("create-request")]
        public async Task<ResponseAPI> CreateRequest([FromBody] CreateRequestSurveyRequestDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new ResponseAPI(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Cập nhật yêu cầu khảo sát
        /// </summary>
        /// /// /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("update-request")]
        public async Task<ResponseAPI> UpdateRequest([FromBody] UpdateRequestSurveyRequestDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new ResponseAPI(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Xóa (Hủy/Đóng) yêu cầu khảo sát
        /// </summary>
        /// /// /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete("delete-request/{id}")]
        public async Task<ResponseAPI> DeleteRequest([FromRoute] int id)
        {
            try
            {
                var command = new DeleteRequestSurveyDto { Id = id };
                await _mediator.Send(command);
                return new ResponseAPI("Đã hủy yêu cầu khảo sát thành công.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Gửi duyệt yêu cầu (Draft -> Pending)
        /// </summary>
        /// /// /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("submit-request/{id}")]
        public async Task<ResponseAPI> SubmitRequest([FromRoute] int id)
        {
            try
            {
                var command = new SubmitRequestDto(id);
                await _mediator.Send(command);
                return new ResponseAPI("Đã gửi duyệt thành công.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Hủy gửi duyệt (Pending -> Draft)
        /// </summary>
        /// /// /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("cancel-submit/{id}")]
        public async Task<ResponseAPI> CancelSubmitRequest([FromRoute] int id)
        {
            try
            {
                var command = new CancelSubmitRequestDto(id);
                await _mediator.Send(command);
                return new ResponseAPI("Đã hủy gửi duyệt, phiếu về trạng thái nháp.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Duyệt (Pending -> Approved)
        /// </summary>
        /// /// /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("approve-request/{id}")]
        public async Task<ResponseAPI> ApproveRequest([FromRoute] int id)
        {
            try
            {
                await _mediator.Send(new ApproveRequestDto(id));
                return new ResponseAPI("Đã duyệt yêu cầu và tạo khảo sát thành công.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Từ chối (Pending -> Rejected)
        /// </summary>
        /// /// /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("reject-request")]
        public async Task<ResponseAPI> RejectRequest([FromBody] RejectRequestDto dto)
        {
            try
            {
                await _mediator.Send(dto);
                return new ResponseAPI("Đã từ chối yêu cầu.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Lấy danh sách khảo sát (Paging)
        /// </summary>
        [HttpGet("paging-survey")]
        public async Task<ResponseAPI> PagingSurvey([FromQuery] FilterSurveyDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new ResponseAPI(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Lấy chi tiết khảo sát
        /// </summary>
        [HttpGet("get-survey-by-id/{id}")]
        public async Task<ResponseAPI> GetSurveyById([FromRoute] int id)
        {
            try
            {
                var query = new GetSurveyDetailDto(id);
                var result = await _mediator.Send(query);
                return new ResponseAPI(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Mở khảo sát
        /// </summary>
        [HttpPost("open-survey/{id}")]
        public async Task<IActionResult> OpenSurvey([FromRoute] int id)
        {
            try
            {
                await _mediator.Send(new OpenSurveyDto(id));
                return Ok(new ResponseAPI("Đã mở khảo sát thành công."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(ex.Message));
            }
        }

        /// <summary>
        /// Đóng khảo sát
        /// </summary>
        [HttpPost("close-survey/{id}")]
        public async Task<IActionResult> CloseSurvey([FromRoute] int id)
        {
            try
            {
                await _mediator.Send(new CloseSurveyDto(id));
                return Ok(new ResponseAPI("Đã đóng khảo sát thành công."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(ex.Message));
            }
        }

        /// <summary>
        /// Lấy danh sách khảo sát phải làm (Paging)
        /// </summary>
        [HttpGet("paging-my-surveys")]
        public async Task<ResponseAPI> GetMySurveys([FromQuery] FilterMySurveyDto query)
        {
            try
            {
                var result = await _mediator.Send(query);
                return new ResponseAPI(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Bắt đầu khảo sát (lấy thông tin khảo sát để làm)
        /// </summary>
        [HttpPost("start-survey/{surveyId}")]
        public async Task<ResponseAPI> StartSurvey([FromRoute] int surveyId)
        {
            try
            {
                var command = new StartSurveyDto(surveyId);
                var result = await _mediator.Send(command);
                return new ResponseAPI(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Lưu nháp khảo sát
        /// </summary>
        [HttpPost("save-draft-survey")]
        public async Task<ResponseAPI> SaveDraft([FromBody] SubmitSurveyRequestDto dto)
        {
            try
            {
                var command = new SaveDraftDto
                {
                    SubmissionId = dto.SubmissionId,
                    Answers = dto.Answers
                };
                await _mediator.Send(command);
                return new ResponseAPI("Lưu nháp thành công.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Nộp khảo sát
        /// </summary>
        [HttpPost("submit-survey")]
        public async Task<ResponseAPI> SubmitSurvey([FromBody] SubmitSurveyRequestDto dto)
        {
            try
            {
                var command = new SubmitSurveyDto
                {
                    SubmissionId = dto.SubmissionId,
                    Answers = dto.Answers
                };
                var result = await _mediator.Send(command);
                return new ResponseAPI(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Tạo báo cáo khảo sát
        /// </summary>
        [HttpPost("generate-report/{surveyId}")]
        public async Task<IActionResult> GenerateReport([FromRoute] int surveyId)
        {
            try
            {
                await _mediator.Send(new GenerateReportDto(surveyId));
                return Ok(new ResponseAPI("Đã tạo báo cáo thành công."));               
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(ex.Message));
            }
        }

        /// <summary>
        /// Lấy danh sách báo cáo khảo sát (Paging)
        /// </summary>
        [HttpGet("paging-report")]
        public async Task<ResponseAPI> PagingReport([FromQuery] FilterReportSurveyDto query)
        {
            try
            {
                var result = await _mediator.Send(query);
                return new ResponseAPI(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Lấy chi tiết báo cáo khảo sát theo Id
        /// </summary>
        [HttpGet("get-report-by-id/{id}")]
        public async Task<IActionResult> GetReportDetail([FromRoute] int id)
        {
            try
            {
                var query = new GetReportDetailDto(id);
                var result = await _mediator.Send(new GetReportDetailDto(id));
                return Ok(new ResponseAPI(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(ex.Message));
            }
        }

    }
}
