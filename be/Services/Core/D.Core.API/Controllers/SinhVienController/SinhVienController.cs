using D.ControllerBase;
using D.Core.Domain.Dtos.SinhVien;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.API.Controllers.SinhVienController
{
    [Route("/api/sinhvien")]
    [ApiController]
    public class SvSinhVienController : APIControllerBase
    {
        private IMediator _mediator;

        public SvSinhVienController(IMediator mediator)
            : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("find")]
        public async Task<ResponseAPI> GetListNhanSu(SvSinhVienRequestDto dto)
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

        [HttpPost("create")]
        public async Task<ResponseAPI> Create([FromBody] SvSinhVienCreateRequestDto dto)
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

        [HttpPut("update")]
        public async Task<ResponseAPI> Update([FromBody] SvSinhVienUpdateRequestDto dto)
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

        [HttpDelete("delete")]
        public async Task<ResponseAPI> Delete([FromBody] SvSinhVienDeleteRequestDto dto)
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

        [HttpGet("detail")]
        public async Task<ResponseAPI> GetDetail([FromQuery] SvSinhVienGetByIdRequestDto dto)
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
