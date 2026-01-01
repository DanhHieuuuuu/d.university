using D.ControllerBase;
using D.Core.Domain.Dtos.Kpi.KpiDonVi;
using D.Core.Domain.Dtos.Kpi.KpiRole;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace D.Core.API.Controllers.Kpi
{
    [Route("/api/kpi")]
    [ApiController]
    public class KpiRoleController : APIControllerBase
    {
        private IMediator _mediator;

        public KpiRoleController(IMediator mediator)
            : base(mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Danh sách Kpi Role
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("kpi-role/find")]
        public async Task<ResponseAPI> GetAllKpiRole([FromQuery] KpiRoleRequestDto dto)
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
        /// Thêm Kpi Role
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("kpi-role/create")]
        public async Task<ResponseAPI> CreateKpiRole([FromBody] CreateKpiRoleDto dto)
        {
            try
            {
                await _mediator.Send(dto);
                return new("Thêm role Kpi thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Cập nhật Kpi Role
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("kpi-role/update")]
        public async Task<ResponseAPI> UpdateKpiRole([FromBody] UpdateKpiRoleDto dto)
        {
            try
            {
                await _mediator.Send(dto);
                return new("Đã cập nhật Role KPI thành công!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Xóa mềm Kpi Role
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("kpi-role/delete")]
        public async Task<ResponseAPI> DeleteKpiRole([FromBody] DeleteKpiRoleDto dto)
        {
            try
            {
                await _mediator.Send(dto);
                return new("Đã xóa Role KPI thành công!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Danh sách Kpi Role của User
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("kpi-role/list-role-by-user")]
        public async Task<ResponseAPI> GetListRoleByUser([FromQuery] GetKpiRoleByUserRequestDto dto)
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
