using D.ControllerBase;
using D.Core.Domain.Dtos.Kpi.KpiTruong;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.API.Controllers.Kpi
{
    [Route("/api/kpi")]
    [ApiController]
    public class KpiTruongController : APIControllerBase
    {
        private IMediator _mediator;

        public KpiTruongController(IMediator mediator)
            : base(mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Thêm Kpi Trường
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("kpi-truong/create")]
        public async Task<ResponseAPI> CreateKpiTruong([FromBody] CreateKpiTruongDto dto)
        {
            try
            {
                await _mediator.Send(dto);
                return new("Thêm Kpi trường thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Danh sách Kpi trường
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("kpi-truong/find")]
        public async Task<ResponseAPI> GetAllKpiTruong([FromQuery] FilterKpiTruongDto dto)
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
        /// Danh sách Kpi trường thành list
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("kpi-truong/get-list-all")]
        public async Task<ResponseAPI> GetAllListKpiTruong([FromQuery] GetListKpiTruongRequestDto dto)
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
        /// Cập nhật Kpi trường
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("kpi-truong/update")]
        public async Task<ResponseAPI> UpdateKpiTruong([FromBody] UpdateKpiTruongDto dto)
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
        /// Xóa mềm Kpi trường
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("kpi-truong/delete")]
        public async Task<ResponseAPI> DeleteKpiTruong([FromBody] DeleteKpiTruongDto dto)
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
        /// Cập nhật Kpi trường
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("kpi-truong/update-trang-thai")]
        public async Task<ResponseAPI> UpdateTrangThai([FromBody] UpdateTrangThaiKpiTruongDto dto)
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
        /// Cập nhật kết quả thực tế Kpi trường
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("kpi-truong/update-ket-qua-thuc-te")]
        public async Task<ResponseAPI> UpdateKetQuaThucTe([FromBody] UpdateKpiThucTeKpiTruongListDto dto)
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
        /// Cập nhật kết quả cấp trên Kpi truong
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("kpi-truong/update-ket-qua-cap-tren")]
        public async Task<ResponseAPI> UpdateKetQuaCapTren([FromBody] UpdateKetQuaCapTrenKpiTruongListDto dto)
        {
            try
            {
                await _mediator.Send(dto);
                return new("Đã thêm kết quả cấp trên thành công!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Danh sách trạng thái Kpi truong
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("kpi-truong/trang-thai")]
        public async Task<ResponseAPI> GetTrangThai([FromQuery] GetTrangThaiKpiTruongDto  dto)
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
        [HttpGet("kpi-truong/list-nam-hoc")]
        public async Task<ResponseAPI> GetNamHoc([FromQuery] GetListYearKpiTruongRequestDto dto)
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
        /// Giao Kpi Trường cho hiệu trưởng
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("kpi-truong/giao-kpi-hieu-truong")]
        public async Task<ResponseAPI> GiaoKpiHieuTruong([FromBody] GiaoKpiHieuTruongDto dto)
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
