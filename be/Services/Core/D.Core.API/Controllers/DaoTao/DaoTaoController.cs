using d.Shared.Permission;
using D.ControllerBase;
using D.Core.Domain.Dtos.DaoTao.ChuyenNganh;
using D.Core.Domain.Dtos.DaoTao.Khoa;
using D.Core.Domain.Dtos.DaoTao.Nganh;
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
        #region Khoa
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
        #endregion

        #region Ngành
        /// <summary>
        /// Danh sách ngành
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("nganh/find")]
        public async Task<ResponseAPI> GetAllNganh([FromQuery] DtNganhRequestDto dto)
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
        /// Lấy ngành theo Id
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        [HttpGet("nganh/get-by-id")]
        public async Task<ResponseAPI> GetDetailDtNganh(
            [FromQuery] DtNganhGetByIdRequestDto dto
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
        /// Tạo mới ngành
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("nganh/create")]
        public async Task<ResponseAPI> CreateDtNganh([FromBody] CreateDtNganhDto dto)
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
        /// Cập nhật ngành
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("nganh/update")]
        public async Task<ResponseAPI> UpdateDtNganh([FromBody] UpdateDtNganhDto dto)
        {
            try
            {
                await _mediator.Send(dto);
                return new("Đã cập nhật ngành thành công!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Xóa ngành
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete("nganh/delete/{id}")]
        public async Task<ResponseAPI> DeleteDtNganh([FromRoute] int id)
        {
            try
            {
                var dto = new DeleteDtNganhDto { Id = id };
                await _mediator.Send(dto);
                return new("Đã xóa ngành.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region ChuyenNganh
        /// <summary>
        /// Danh sách chuyên ngành
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("chuyen-nganh/find")]
        public async Task<ResponseAPI> GetAllChuyenNganh([FromQuery] DtChuyenNganhRequestDto dto)
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
        /// Lấy chuyên ngành theo Id
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        [HttpGet("chuyen-nganh/get-by-id")]
        public async Task<ResponseAPI> GetDetailDtChuyenNganh(
            [FromQuery] DtChuyenNganhGetByIdRequestDto dto
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
        /// Tạo mới chuyên ngành
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("chuyen-nganh/create")]
        public async Task<ResponseAPI> CreateDtChuyenNganh([FromBody] CreateDtChuyenNganhDto dto)
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
        /// Cập nhật chuyên ngành
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("chuyen-nganh/update")]
        public async Task<ResponseAPI> UpdateDtChuyenNganh([FromBody] UpdateDtChuyenNganhDto dto)
        {
            try
            {
                await _mediator.Send(dto);
                return new("Đã cập nhật chuyên ngành thành công!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Xóa chuyên ngành
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete("chuyen-nganh/delete/{id}")]
        public async Task<ResponseAPI> DeleteDtChuyenNganh([FromRoute] int id)
        {
            try
            {
                var dto = new DeleteDtChuyenNganhDto { Id = id };
                await _mediator.Send(dto);
                return new("Đã xóa chuyên ngành.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion
    }
}
