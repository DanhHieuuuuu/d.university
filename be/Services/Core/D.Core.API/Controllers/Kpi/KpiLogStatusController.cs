using D.ControllerBase;
using D.Core.Domain.Dtos.Kpi.KpiLogStatus;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.API.Controllers.Kpi
{
    [ApiController]
    [Route("/api/kpi")]
    public class KpiLogStatusController : APIControllerBase
    {
        private IMediator _mediator;

        public KpiLogStatusController(IMediator mediator)
            : base(mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Lấy log status
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        [HttpGet("kpi-log/get-log-status")]
        public async Task<ResponseAPI> GetLogStatus([FromQuery] FindKpiLogStatusDto dto)
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
