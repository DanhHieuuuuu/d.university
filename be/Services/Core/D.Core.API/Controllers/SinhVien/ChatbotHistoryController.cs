using D.ControllerBase;
using D.Core.Domain.Dtos.SinhVien.ChatbotHistory;
using D.Core.Infrastructure.Services.SinhVien.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.API.Controllers.SinhVien
{
    [Route("/api/chatbot")]
    [ApiController]
    public class ChatbotHistoryController : APIControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IChatbotService _chatbotService;

        public ChatbotHistoryController(
            IMediator mediator,
            IChatbotService chatbotService
        ) : base(mediator)
        {
            _mediator = mediator;
            _chatbotService = chatbotService;
        }

        /// <summary>
        /// Chat với chatbot AI
        /// </summary>
        [HttpPost("chat")]
        public async Task<ResponseAPI> Chat([FromBody] ChatbotRequestDto dto)
        {
            try
            {
                var result = await _chatbotService.ChatAsync(dto);
                return new ResponseAPI(result);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new Exception($"Không thể kết nối đến Chatbot API: {ex.Message}"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Đồng bộ tất cả thông tin sinh viên vào ChromaDB
        /// </summary>
        [HttpGet("sync-students")]
        public async Task<ResponseAPI> SyncStudents()
        {
            try
            {
                var result = await _chatbotService.SyncAllStudentsAsync();
                return new ResponseAPI(result);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new Exception($"Không thể kết nối đến Chatbot API: {ex.Message}"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
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

        #region ChatbotModel CRUD

        /// <summary>
        /// Lấy Chatbot Model đang được chọn (IsSelected = true)
        /// </summary>
        [HttpGet("model/selected")]
        public async Task<ResponseAPI> GetSelectedModel()
        {
            try
            {
                var result = await _mediator.Send(new GetSelectedSvChatbotModelDto());
                return new ResponseAPI(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Lấy danh sách tất cả Chatbot Model
        /// </summary>
        [HttpGet("model/list")]
        public async Task<ResponseAPI> GetAllModels()
        {
            try
            {
                var result = await _mediator.Send(new GetAllSvChatbotModelDto());
                return new ResponseAPI(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Lấy Chatbot Model theo Id
        /// </summary>
        [HttpGet("model/{id}")]
        public async Task<ResponseAPI> GetModelById(int id)
        {
            try
            {
                var result = await _mediator.Send(new GetSvChatbotModelByIdDto { Id = id });
                return new ResponseAPI(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Thêm mới Chatbot Model
        /// </summary>
        [HttpPost("model/create")]
        public async Task<ResponseAPI> CreateModel([FromBody] CreateSvChatbotModelDto dto)
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
        /// Cập nhật Chatbot Model
        /// </summary>
        [HttpPut("model/update")]
        public async Task<ResponseAPI> UpdateModel([FromBody] UpdateSvChatbotModelDto dto)
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
        /// Xóa Chatbot Model
        /// </summary>
        [HttpDelete("model/delete/{id}")]
        public async Task<ResponseAPI> DeleteModel(int id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteSvChatbotModelDto { Id = id });
                return new ResponseAPI(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        #endregion
    }
}
