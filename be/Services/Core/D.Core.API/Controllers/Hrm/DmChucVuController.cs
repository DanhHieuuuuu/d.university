using D.ControllerBases;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmChucVu;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.API.Controllers.Hrm
{
    [Route("/api/chucvu")]
    [ApiController]
    public class DmChucVuController : APIControllerBase
    {
        private IMediator _mediator;

        public DmChucVuController(IMediator mediator)
            : base(mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// Lấy chức vụ theo Id
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>


        [HttpGet("get-by-id")]
        public async Task<ResponseAPI> GetDetail([FromQuery] DmChucVuGetByIdRequestDto dto)
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
