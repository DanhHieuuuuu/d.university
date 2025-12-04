using D.ControllerBase;
using D.Core.Domain.Dtos.Kpi.KpiCaNhan;
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
    }
}
