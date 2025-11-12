using D.ControllerBase;
using D.Core.Application.Query.Noti;
using D.Core.Domain.Dtos.Noti;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.API.Controllers.Notification
{
    [Route("/api/noti")]
    [ApiController]
    public class NotificationController : APIControllerBase
    {
        private IMediator _mediator;

        public NotificationController(IMediator mediator)
            : base(mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Danh sách thông báo của tôi
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("find")]
        public async Task<ResponseAPI> MyListNotification([FromQuery] NotiRequestDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Đánh dấu đã đọc
        /// </summary>
        /// <param name="id">Id thông báo</param>
        /// <returns></returns>
        [HttpPut("read/{id}")]
        public async Task<ResponseAPI> MarkAsReadNotification([FromRoute] int id)
        {
            try
            {
                var dto = new MarkAsReadDto { NotificationId = id };
                await _mediator.Send(dto);
                return new();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Đánh dấu tất cả là đã đọc
        /// </summary>
        /// <returns></returns>
        [HttpPut("mark-all-as-read")]
        public async Task<ResponseAPI> MarkAllAsReadNotification()
        {
            try
            {
                var dto = new MarkAllAsReadDto();
                await _mediator.Send(dto);
                return new();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
