using D.ControllerBase;
using D.Core.Domain.Dtos.Kpi.KpiTemplate;
using D.Core.Domain.Dtos.Kpi.KpiTruong;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.API.Controllers.Kpi
{
    [Route("/api/kpi")]
    [ApiController]
    public class KpiTemplateController : APIControllerBase
    {
        private IMediator _mediator;

        public KpiTemplateController(IMediator mediator)
            : base(mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Thêm Kpi Template
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("kpi-template/create")]
        public async Task<ResponseAPI> CreateKpiTemplate([FromBody] CreateKpiTemplateDto dto)
        {
            try
            {
                await _mediator.Send(dto);
                return new("Thêm Kpi template thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Danh sách Kpi Template
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("kpi-template/find")]
        public async Task<ResponseAPI> GetAllKpiTemplate ([FromQuery] FilterKpiTemplateDto dto)
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
        /// Cập nhật Kpi template
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("kpi-template/update")]
        public async Task<ResponseAPI> UpdateKpiTemplate([FromBody] UpdateKpiTemplateDto dto)
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
        /// Xóa mềm Kpi template
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("kpi-template/delete")]
        public async Task<ResponseAPI> DeleteKpiTruong([FromBody] DeleteKpiTemplateDto dto)
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
        /// Danh sách loai Template
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("kpi-template/type")]
        public async Task<ResponseAPI> GetTemplateTypes([FromQuery] GetTemplateTypeRequestDto dto)
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
        /// Giao Kpi Template 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("kpi-template/sync-kpi-template")]
        public async Task<ResponseAPI> SyncKpiTemplate([FromBody] SyncKpiTemplateRequestDto dto)
        {
            try
            {
                await _mediator.Send(dto);
                return new("Đã đồng bộ kpi thành công!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
