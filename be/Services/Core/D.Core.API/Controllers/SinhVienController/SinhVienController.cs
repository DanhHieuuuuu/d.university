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

        /// <summary>
        /// Danh sách sinh viên
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("find")]
        public async Task<ResponseAPI> GetListSinhVien(SvSinhVienRequestDto dto)
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
        /// Danh sách sinh viên bản format
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("get-all")]
        public async Task<ResponseAPI> GetAll(SvSinhVienGetAllRequestDto dto)
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
                return new(result);
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
                return new(result);
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
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Lấy thông tin sinh viên bằng mssv
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("detail")]
        public async Task<ResponseAPI> FindByMssv(FindByMssvDto dto)
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
