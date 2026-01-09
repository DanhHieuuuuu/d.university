using D.ControllerBase;
using D.Core.Domain.Dtos.Hrm.QuyetDinh;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.API.Controllers.Hrm
{
    [Route("/api/decision")]
    [ApiController]
    public class NsQuyetDinhController : APIControllerBase
    {
        private IMediator _mediator;

        public NsQuyetDinhController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("find")]
        public async Task<ResponseAPI> GetAllQuyetDinh(NsQuyetDinhRequestDto dto)
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
    }
}
