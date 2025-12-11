using d.Shared.Permission;
using D.ControllerBase;
using D.Core.Domain.Dtos.DaoTao.ChuongTrinhKhung;
using D.Core.Domain.Dtos.DaoTao.ChuongTrinhKhungMon;
using D.Core.Domain.Dtos.DaoTao.ChuyenNganh;
using D.Core.Domain.Dtos.DaoTao.Khoa;
using D.Core.Domain.Dtos.DaoTao.MonHoc;
using D.Core.Domain.Dtos.DaoTao.MonTienQuyet;
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

        #region MonHoc
        /// <summary>
        /// Danh sách môn học
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("mon-hoc/find")]
        public async Task<ResponseAPI> GetAllMonHoc([FromQuery] DtMonHocRequestDto dto)
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
        /// Lấy môn học theo Id
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        [HttpGet("mon-hoc/get-by-id")]
        public async Task<ResponseAPI> GetDetailDtMonHoc(
            [FromQuery] DtMonHocGetByIdRequestDto dto
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
        /// Tạo mới môn học
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("mon-hoc/create")]
        public async Task<ResponseAPI> CreateDtMonHoc([FromBody] CreateDtMonHocDto dto)
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
        /// Cập nhật môn học
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("mon-hoc/update")]
        public async Task<ResponseAPI> UpdateDtMonHoc([FromBody] UpdateDtMonHocDto dto)
        {
            try
            {
                await _mediator.Send(dto);
                return new("Đã cập nhật môn học thành công!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Xóa môn học
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete("mon-hoc/delete/{id}")]
        public async Task<ResponseAPI> DeleteDtMonHoc([FromRoute] int id)
        {
            try
            {
                var dto = new DeleteDtMonHocDto { Id = id };
                await _mediator.Send(dto);
                return new("Đã xóa môn học.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region MonTienQuyet
        /// <summary>
        /// Danh sách môn tiên quyết
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("mon-tien-quyet/find")]
        public async Task<ResponseAPI> GetAllMonTienQuyet([FromQuery] DtMonTienQuyetRequestDto dto)
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
        /// Lấy môn tiên quyết theo Id
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        [HttpGet("mon-tien-quyet/get-by-id")]
        public async Task<ResponseAPI> GetDetailDtMonTienQuyet(
            [FromQuery] DtMonTienQuyetGetByIdRequestDto dto
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
        /// Tạo mới môn tiên quyết
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("mon-tien-quyet/create")]
        public async Task<ResponseAPI> CreateDtMonTienQuyet([FromBody] CreateDtMonTienQuyetDto dto)
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
        /// Cập nhật môn tiên quyết
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("mon-tien-quyet/update")]
        public async Task<ResponseAPI> UpdateDtMonTienQuyet([FromBody] UpdateDtMonTienQuyetDto dto)
        {
            try
            {
                await _mediator.Send(dto);
                return new("Đã cập nhật môn tiên quyết thành công!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Xóa môn tiên quyết
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete("mon-tien-quyet/delete/{id}")]
        public async Task<ResponseAPI> DeleteDtMonTienQuyet([FromRoute] int id)
        {
            try
            {
                var dto = new DeleteDtMonTienQuyetDto { Id = id };
                await _mediator.Send(dto);
                return new("Đã xóa môn tiên quyết.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region ChuongTrinhKhung
        /// <summary>
        /// Danh sách chương trình khung
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("chuong-trinh-khung/find")]
        public async Task<ResponseAPI> GetAllChuongTrinhKhung([FromQuery] DtChuongTrinhKhungRequestDto dto)
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
        /// Lấy chương trình khung theo Id
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        [HttpGet("chuong-trinh-khung/get-by-id")]
        public async Task<ResponseAPI> GetDetailDtChuongTrinhKhung(
            [FromQuery] DtChuongTrinhKhungGetByIdRequestDto dto
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
        /// Tạo mới chương trình khung
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("chuong-trinh-khung/create")]
        public async Task<ResponseAPI> CreateDtChuongTrinhKhung([FromBody] CreateDtChuongTrinhKhungDto dto)
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
        /// Cập nhật chương trình khung
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("chuong-trinh-khung/update")]
        public async Task<ResponseAPI> UpdateDtChuongTrinhKhung([FromBody] UpdateDtChuongTrinhKhungDto dto)
        {
            try
            {
                await _mediator.Send(dto);
                return new("Đã cập nhật chương trình khung thành công!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Xóa chương trình khung
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete("chuong-trinh-khung/delete/{id}")]
        public async Task<ResponseAPI> DeleteDtChuongTrinhKhung([FromRoute] int id)
        {
            try
            {
                var dto = new DeleteDtChuongTrinhKhungDto { Id = id };
                await _mediator.Send(dto);
                return new("Đã xóa chương trình khung.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region ChuongTrinhKhungMon
        /// <summary>
        /// Danh sách chương trình khung môn
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("chuong-trinh-khung-mon/find")]
        public async Task<ResponseAPI> GetAllChuongTrinhKhungMon([FromQuery] DtChuongTrinhKhungMonRequestDto dto)
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
        /// Lấy chương trình khung môn theo Id
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        [HttpGet("chuong-trinh-khung-mon/get-by-id")]
        public async Task<ResponseAPI> GetDetailDtChuongTrinhKhungMon(
            [FromQuery] DtChuongTrinhKhungMonGetByIdRequestDto dto
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
        /// Tạo mới chương trình khung môn
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("chuong-trinh-khung-mon/create")]
        public async Task<ResponseAPI> CreateDtChuongTrinhKhungMon([FromBody] CreateDtChuongTrinhKhungMonDto dto)
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
        /// Cập nhật chương trình khung môn
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("chuong-trinh-khung-mon/update")]
        public async Task<ResponseAPI> UpdateDtChuongTrinhKhungMon([FromBody] UpdateDtChuongTrinhKhungMonDto dto)
        {
            try
            {
                await _mediator.Send(dto);
                return new("Đã cập nhật chương trình khung môn thành công!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Xóa chương trình khung môn
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete("chuong-trinh-khung-mon/delete/{id}")]
        public async Task<ResponseAPI> DeleteDtChuongTrinhKhungMon([FromRoute] int id)
        {
            try
            {
                var dto = new DeleteDtChuongTrinhKhungMonDto { Id = id };
                await _mediator.Send(dto);
                return new("Đã xóa chương trình khung môn.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion
    }
}
