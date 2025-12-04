using d.Shared.Permission;
using D.ControllerBase;
using D.Core.Domain.Dtos.DaoTao.Khoa;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.API.Controllers.DaoTao
{
    [Route("/api/daotao")]
    [ApiController]
    public class DaoTaoController : APIControllerBase
    {
        private readonly IMediator _mediator;

        public DaoTaoController(IMediator mediator)
            : base(mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Danh sách khoa
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("khoa/find")]
        public async Task<ResponseAPI> GetAllKhoa([FromQuery] DtKhoaRequestDto dto)
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
        /// Lấy khoa theo Id
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        [HttpGet("khoa/get-by-id")]
        public async Task<ResponseAPI> GetDetailDtKhoa(
            [FromQuery] DtKhoaGetByIdRequestDto dto
        )
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
        /// Tạo mới khoa
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("khoa/create")]
        public async Task<ResponseAPI> CreateDtKhoa([FromBody] CreateDtKhoaDto dto)
        {
            try
            {
                await _mediator.Send(dto);
                return new();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Cập nhật khoa
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("khoa/update")]
        public async Task<ResponseAPI> UpdateDtKhoa([FromBody] UpdateDtKhoaDto dto)
        {
            try
            {
                await _mediator.Send(dto);
                return new("Đã cập nhật khoa thành công!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Xóa khoa
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete("khoa/delete/{id}")]
        public async Task<ResponseAPI> DeleteDtKhoa([FromRoute] int id)
        {
            try
            {
                var dto = new DeleteDtKhoaDto { Id = id };
                await _mediator.Send(dto);
                return new("Đã xóa khoa.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
