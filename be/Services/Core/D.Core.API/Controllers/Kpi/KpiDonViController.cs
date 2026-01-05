using D.ControllerBase;
using D.Core.Domain.Dtos.Kpi.KpiDonVi;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.API.Controllers.Kpi
{
    [Route("/api/kpi")]
    [ApiController]
    public class KpiDonViController : APIControllerBase
    {
        private IMediator _mediator;

        public KpiDonViController(IMediator mediator)
            : base(mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Thêm Kpi Role
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("kpi-donvi/create")]
        public async Task<ResponseAPI> CreateKpiDonVi([FromBody] CreateKpiDonViDto dto)
        {
            try
            {
                await _mediator.Send(dto);
                return new("Thêm Kpi đơn vị thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Danh sách Kpi đơn vị
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("kpi-donvi/find")]
        public async Task<ResponseAPI> GetAllKpiDonVi([FromQuery] FilterKpiDonViDto dto)
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
        /// Cập nhật Kpi đơn vị
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("kpi-donvi/update")]
        public async Task<ResponseAPI> UpdateKpiDonVi([FromBody] UpdateKpiDonViDto dto)
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
        /// Xóa mềm Kpi đơn vị
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("kpi-donvi/delete")]
        public async Task<ResponseAPI> DeleteKpiDonVi([FromBody] DeleteKpiDonViDto dto)
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
        /// Cập nhật Kpi đơn vị
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("kpi-donvi/update-trang-thai")]
        public async Task<ResponseAPI> UpdateTrangThai([FromBody] UpdateTrangThaiKpiDonViDto dto)
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
        /// Cập nhật kết quả thực tế Kpi đơn vị
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("kpi-donvi/update-ket-qua-thuc-te")]
        public async Task<ResponseAPI> UpdateKetQuaThucTe([FromBody] UpdateKpiThucTeKpiDonViListDto dto)
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
        /// Danh sách trạng thái Kpi đơn vị
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("kpi-donvi/trang-thai")]
        public async Task<ResponseAPI> GetTrangThai([FromQuery] GetTrangThaiKpiDonViDto dto)
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
        /// Danh sách trạng thái Kpi đơn vị
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("kpi-donvi/list-nam-hoc")]
        public async Task<ResponseAPI> GetNamHoc([FromQuery] GetListYearKpiDonViRequestDto dto)
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
        /// Danh sách Kpi đơn vị kê khai của người dùng
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("kpi-donvi/find-ke-khai")]
        public async Task<ResponseAPI> GetKpiDonViKeKhai([FromQuery] FilterKpiDonViKeKhaiDto dto)
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
        /// Danh sách người đã giao Kpi đơn vị
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("kpi-donvi/nhan-su-da-giao")]
        public async Task<ResponseAPI> GetNhanSuFromKpiDonVi([FromQuery] GetNhanSuFromKpiDonViDto dto)
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
        /// Giao Kpi Đơn Vị cho Nhân sự
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("kpi-donvi/giao-kpi")]
        public async Task<ResponseAPI> GiaoKpiDonVi([FromBody] GiaoKpiDonViDto dto)
        {
            try
            {
                await _mediator.Send(dto);
                return new("Đã giao kpi thành công!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
