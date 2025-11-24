using d.Shared.Permission;
using D.ControllerBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmChucVu;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmDanToc;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmGioiTinh;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmKhoaHoc;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmLoaiHopDong;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmPhongBan;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmQuanHeGiaDinh;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmQuocTich;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmToBoMon;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmTonGiao;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.API.Controllers.Hrm
{
    [Route("/api/danhmuc")]
    [ApiController]
    public class DanhMucController : APIControllerBase
    {
        private IMediator _mediator;

        public DanhMucController(IMediator mediator)
            : base(mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Danh sách chức vụ
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("chuc-vu/find")]
        public async Task<ResponseAPI> GetAllChucVu([FromQuery] DmChucVuRequestDto dto)
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
        /// Lấy chức vụ theo Id
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("chuc-vu/get-by-id")]
        public async Task<ResponseAPI> GetDetailDmChucVu([FromQuery] DmChucVuGetByIdRequestDto dto)
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
        /// Thêm mới chức vụ
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [PermissionFilter(PermissionKeyConstant.Admin)]
        [HttpPost("chuc-vu/create")]
        public async Task<ResponseAPI> CreateDmChucVu([FromBody] CreateDmChucVuDto dto)
        {
            try
            {
                await _mediator.Send(dto);
                return new("Thêm mới chức vụ thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Cập nhật thông tin chức vụ
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [PermissionFilter(PermissionKeyConstant.Admin)]
        [HttpPut("chuc-vu/update")]
        public async Task<ResponseAPI> UpdateDmChucVu([FromBody] UpdateDmChucVuDto dto)
        {
            try
            {
                await _mediator.Send(dto);
                return new("Đã cập nhật chức vụ thành công!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Xóa mềm chức vụ
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [PermissionFilter(PermissionKeyConstant.Admin)]
        [HttpDelete("chuc-vu/delete/{id}")]
        public async Task<ResponseAPI> DeleteDmChucVu([FromRoute] int id)
        {
            try
            {
                var dto = new DeleteDmPhongBanDto { Id = id };
                await _mediator.Send(dto);
                return new("Đã xóa chức vụ.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Danh sách dân tộc
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("dan-toc")]
        public async Task<ResponseAPI> GetAllDanToc([FromQuery] DmDanTocRequestDto dto)
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
        /// Danh sách giới tính
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("gioi-tinh")]
        public async Task<ResponseAPI> GetAllGioiTinh([FromQuery] DmGioiTinhRequestDto dto)
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
        /// Danh sách các loại hợp đồng
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("loai-hop-dong")]
        public async Task<ResponseAPI> GetAllLoaiHopDong([FromQuery] DmLoaiHopDongRequestDto dto)
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
        /// Danh sách loại phòng, ban
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("loai-phong-ban")]
        public async Task<ResponseAPI> GetAllLoaiPhongBan([FromQuery] DmLoaiPhongBanRequestDto dto)
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
        /// Danh sách phòng, ban
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("phong-ban/find")]
        public async Task<ResponseAPI> GetAllPhongBan([FromQuery] DmPhongBanRequestDto dto)
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
        /// Lấy phòng ban theo Id
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        [HttpGet("phong-ban/get-by-id")]
        public async Task<ResponseAPI> GetDetailDmPhongBan(
            [FromQuery] DmPhongBanGetByIdRequestDto dto
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
        /// Tạo mới phòng, ban
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [PermissionFilter(PermissionKeyConstant.Admin)]
        [HttpPost("phong-ban/create")]
        public async Task<ResponseAPI> CreateDmPhongBan([FromBody] CreateDmPhongBanDto dto)
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
        /// Cập nhật phòng ban
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [PermissionFilter(PermissionKeyConstant.Admin)]
        [HttpPut("phong-ban/update")]
        public async Task<ResponseAPI> UpdateDmPhongBan([FromBody] UpdateDmPhongBanDto dto)
        {
            try
            {
                await _mediator.Send(dto);
                return new("Đã cập nhật phòng ban thành công!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Xóa phòng ban
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [PermissionFilter(PermissionKeyConstant.Admin)]
        [HttpDelete("phong-ban/delete/{id}")]
        public async Task<ResponseAPI> DeleteDmPhongBan([FromRoute] int id)
        {
            try
            {
                var dto = new DeleteDmPhongBanDto { Id = id };
                await _mediator.Send(dto);
                return new("Đã xóa phòng ban.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Danh sách các mối quan hệ gia đình
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("quan-he")]
        public async Task<ResponseAPI> GetAllQuanHeGiaDinh(
            [FromQuery] DmQuanHeGiaDinhRequestDto dto
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
        /// Danh sách quốc tịch
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("quoc-tich")]
        public async Task<ResponseAPI> GetAllQuocTich([FromQuery] DmQuocTichRequestDto dto)
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
        /// Danh sách tổ bộ môn
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("to-bo-mon/find")]
        public async Task<ResponseAPI> GetAllToBoMon([FromQuery] DmToBoMonRequestDto dto)
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
        /// Thêm mới tổ bộ môn
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [PermissionFilter(PermissionKeyConstant.Admin)]
        [HttpPost("to-bo-mon/create")]
        public async Task<ResponseAPI> CreateDmToBoMon([FromBody] CreateDmToBoMonDto dto)
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
        /// Danh sách tôn giáo
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("ton-giao")]
        public async Task<ResponseAPI> GetAllTonGiao([FromQuery] DmTonGiaoRequestDto dto)
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
        /// Danh sách khóa
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("khoa-hoc/find")]
        public async Task<ResponseAPI> GetAllKhoa([FromQuery] DmKhoaHocRequestDto dto)
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
        /// Lấy khóa theo Id
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("khoa-hoc/get-by-id")]
        public async Task<ResponseAPI> GetDetailDmKhoaHoc([FromQuery] DmKhoaHocGetByIdRequestDto dto)
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
        /// Thêm mới khóa
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [PermissionFilter(PermissionKeyConstant.Admin)]
        [HttpPost("khoa-hoc/create")]
        public async Task<ResponseAPI> CreateDmKhoaHoc([FromBody] CreateDmKhoaHocDto dto)
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
    }
}
