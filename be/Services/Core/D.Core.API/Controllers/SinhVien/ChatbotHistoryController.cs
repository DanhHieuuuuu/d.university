using D.ControllerBase;
using D.Core.Domain.Dtos.SinhVien.ChatbotHistory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.API.Controllers.SinhVien
{
    [Route("/api/chatbot-history")]
    [ApiController]
    public class ChatbotHistoryController : APIControllerBase
    {
        private readonly IMediator _mediator;

        public ChatbotHistoryController(IMediator mediator)
            : base(mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Lấy lịch sử chat theo SessionId
        /// </summary>
        [HttpGet("by-session")]
        public async Task<ResponseAPI> GetBySession([FromQuery] GetSvChatbotHistoryBySessionDto dto)
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
        /// Lấy lịch sử chat theo MSSV
        /// </summary>
        [HttpGet("by-mssv")]
        public async Task<ResponseAPI> GetByMssv([FromQuery] GetSvChatbotHistoryByMssvDto dto)
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
        /// Lấy danh sách các session của sinh viên
        /// </summary>
        [HttpGet("sessions")]
        public async Task<ResponseAPI> GetSessions([FromQuery] GetSvChatbotSessionsDto dto)
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
        /// Thêm mới lịch sử chat
        /// </summary>
        [HttpPost("create")]
        public async Task<ResponseAPI> Create([FromBody] CreateSvChatbotHistoryDto dto)
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
        /// Cập nhật lịch sử chat
        /// </summary>
        [HttpPut("update")]
        public async Task<ResponseAPI> Update([FromBody] UpdateSvChatbotHistoryDto dto)
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
        /// Xóa lịch sử chat
        /// </summary>
        [HttpDelete("delete")]
        public async Task<ResponseAPI> Delete([FromQuery] DeleteSvChatbotHistoryDto dto)
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
    }
}
