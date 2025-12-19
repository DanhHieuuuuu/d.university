using D.ControllerBase;
using D.Core.Domain.Dtos.Survey.Request;
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
        public async Task<ResponseAPI> Paging([FromQuery] FilterSurveyRequestDto dto)
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
        public async Task<ResponseAPI> GetById([FromRoute] int id)
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
        public async Task<ResponseAPI> Create([FromBody] CreateRequestSurveyRequestDto dto)
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
        public async Task<ResponseAPI> Update([FromBody] UpdateRequestSurveyRequestDto dto)
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
        public async Task<ResponseAPI> Delete([FromRoute] int id)
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
        [HttpPost("submit/{id}")]
        public async Task<ResponseAPI> Submit([FromRoute] int id)
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
        public async Task<ResponseAPI> CancelSubmit([FromRoute] int id)
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
    }
}
