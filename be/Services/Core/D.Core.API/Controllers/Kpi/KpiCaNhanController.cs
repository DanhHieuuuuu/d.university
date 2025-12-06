using D.ControllerBase;
using D.Core.Application.Command.Kpi.KpiCaNhan;
using D.Core.Domain.Dtos.Kpi.KpiCaNhan;
using D.Core.Domain.Dtos.Kpi.KpiRole;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.API.Controllers.Kpi
{
    [Route("/api/kpi")]
    [ApiController]
    public class KpiCaNhanController : APIControllerBase
    {
        private IMediator _mediator;

        public KpiCaNhanController(IMediator mediator)
            : base(mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Thêm Kpi Role
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("kpi-canhan/create")]
        public async Task<ResponseAPI> CreateKpiCaNhan([FromBody] CreateKpiCaNhanDto dto)
        {
            try
            {
                await _mediator.Send(dto);
                return new("Thêm Kpi cá nhân thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Danh sách Kpi Cá Nhân
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("kpi-canhan/find")]
        public async Task<ResponseAPI> GetAllKpiCaNhan([FromQuery] FilterKpiCaNhanDto dto)
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
        /// Cập nhật Kpi Cá Nhân
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("kpi-canhan/update")]
        public async Task<ResponseAPI> UpdateKpiRole([FromBody] UpdateKpiCaNhanDto dto)
        {
            try
            {
                await _mediator.Send(dto);
                return new("Đã cập nhật KPI thành công!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Xóa mềm Kpi Cá Nhân
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("kpi-canhan/delete")]
        public async Task<ResponseAPI> DeleteKpiRole([FromBody] DeleteKpiCaNhanDto dto)
        {
            try
            {
                await _mediator.Send(dto);
                return new("Đã xóa KPI thành công!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Cập nhật Kpi Cá Nhân
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("kpi-canhan/update-trang-thai")]
        public async Task<ResponseAPI> UpdateTrangThai([FromBody] UpdateTrangThaiKpiDto dto)
        {
            try
            {
                await _mediator.Send(dto);
                return new("Đã cập nhật trạng thái Kpi thành công!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Cập nhật kết quả thực tế Kpi Cá Nhân
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("kpi-canhan/update-ket-qua-thuc-te")]
        public async Task<ResponseAPI> UpdateKetQuaThucTe([FromBody] UpdateKpiThucTeKpiCaNhanListDto dto)
        {
            try
            {
                await _mediator.Send(dto);
                return new("Đã thêm kết quả thực tế thành công!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Danh sách trạng thái Kpi Cá Nhân
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("kpi-canhan/trang-thai")]
        public async Task<ResponseAPI> GetTrangThai([FromQuery] GetTrangThaiDto dto)
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
