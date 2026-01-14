using D.ControllerBase;
using D.Core.Domain.Dtos.Kpi.KpiChat;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.API.Controllers.Kpi
{
    namespace D.Core.API.Controllers.Kpi
    {
        [Route("/api/kpi")]
        [ApiController]
        public class KpiChatController : APIControllerBase
        {
            private readonly IMediator _mediator;

            public KpiChatController(IMediator mediator)
                : base(mediator)
            {
                _mediator = mediator;
            }

            /// <summary>
            /// Hỏi đáp AI về số liệu và thông tin KPI (Phân quyền theo User đăng nhập)
            /// </summary>
            /// <param name="command">Chứa câu hỏi của người dùng</param>
            /// <returns>Câu trả lời từ AI Dify</returns>
            [HttpPost("kpi-chat/ask")]
            public async Task<ResponseAPI> AskKpiAi([FromBody] AskKpiChatCommand command)
            {
                try
                {
                    var result = await _mediator.Send(command);

                    return new ResponseAPI(result);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
        }
    }
}
