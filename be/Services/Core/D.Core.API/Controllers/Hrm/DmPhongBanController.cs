using D.ControllerBases;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmPhongBan;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.API.Controllers.Hrm
{
    [Route("/api/phongban")]
    [ApiController]
    public class DmPhongBanController : APIControllerBase
    {
        private IMediator _mediator;

        public DmPhongBanController(IMediator mediator)
            : base(mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// Lấy phòng ban theo Id
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        [HttpGet("get-by-id")]
        public async Task<ResponseAPI> GetDetail([FromQuery] DmPhongBanGetByIdRequestDto dto)
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
