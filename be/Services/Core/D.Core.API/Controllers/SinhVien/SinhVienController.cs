using D.ControllerBase;
using D.Core.Domain.Dtos.SinhVien;
using D.Core.Domain.Dtos.SinhVien.Auth;
using D.Core.Domain.Dtos.SinhVien.ThongTinChiTiet;
using D.Core.Application.Query.SinhVien.SvSinhVien;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.API.Controllers.SinhVienController
{
    [Route("/api/sinhvien")]
    [ApiController]
    public class SvSinhVienController : APIControllerBase
    {
        private readonly IMediator _mediator;

        public SvSinhVienController(IMediator mediator)
            : base(mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Tìm kiếm và phân trang sinh viên
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("find")]
        public async Task<ResponseAPI> GetListSinhVien(SvSinhVienRequestDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new ResponseAPI(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Danh sách sinh viên
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        //[HttpGet("get-all")]
        //public async Task<ResponseAPI> GetAll(SvSinhVienGetAllRequestDto dto)
        //{
        //    try
        //    {
        //        var result = await _mediator.Send(dto);
        //        return new ResponseAPI(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        /// <summary>
        /// Chi tiết sinh viên bằng mssv
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("detail")]
        public async Task<ResponseAPI> FindByMssv(FindByMssvDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new ResponseAPI(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Lấy thông tin chi tiết sinh viên (bao gồm khoa, ngành, điểm, chương trình khung)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("thong-tin-chi-tiet")]
        public async Task<ResponseAPI> GetThongTinChiTiet(SvThongTinChiTietRequestDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new ResponseAPI(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Thêm mới sinh viên
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<ResponseAPI> CreateStudent(CreateSinhVienDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new ResponseAPI(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Cập nhật thông tin sinh viên
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<ResponseAPI> Update(UpdateSinhVienDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new ResponseAPI(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Xóa thông tin sinh viên
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<ResponseAPI> Delete(DeleteSinhVienDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new ResponseAPI(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Đăng nhập Sinh viên
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ResponseAPI> Login([FromBody] SvLoginRequestDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new ResponseAPI(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Đăng xuất
        /// </summary>
        [HttpGet("logout")]
        public async Task<ResponseAPI> Logout()
        {
            try
            {
                var dto = new SvLogoutRequestDto();
                var result = await _mediator.Send(dto);
                return new ResponseAPI(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Refresh Token
        /// </summary>
        [HttpPost("refresh-token")]
        public async Task<ResponseAPI> RefreshToken([FromBody] SvRefreshTokenRequestDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new ResponseAPI(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Thống kê: số lượng sinh viên, môn học, khoa
        /// </summary>
        [HttpGet("thong-ke")]
        public async Task<ResponseAPI> GetThongKe()
        {
            try
            {
                var result = await _mediator.Send(new SvThongKeRequestDto());
                return new ResponseAPI(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
