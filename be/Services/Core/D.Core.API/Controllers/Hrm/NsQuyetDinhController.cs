using D.ControllerBase;
using D.Core.Domain.Dtos.Hrm.QuyetDinh;
using d.Shared.Permission;
using d.Shared.Permission.Permission;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.API.Controllers.Hrm
{
    [Route("/api/decision")]
    [ApiController]
    public class NsQuyetDinhController : APIControllerBase
    {
        private IMediator _mediator;

        public NsQuyetDinhController(IMediator mediator)
            : base(mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Danh sách các quyết định
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [PermissionFilter(PermissionCoreKeys.CoreMenuHrmDecision)]
        [HttpGet("find")]
        public async Task<ResponseAPI> GetAllQuyetDinh(NsQuyetDinhRequestDto dto)
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
        /// Chi tiết quyết định
        /// </summary>
        /// <param name="idQuyetDinh"></param>
        /// <returns></returns>
        [PermissionFilter(PermissionCoreKeys.CoreButtonViewHrmDecision)]
        [HttpGet("{idQuyetDinh}")]
        public async Task<ResponseAPI> ChiTietQuyetDinh([FromRoute] int idQuyetDinh)
        {
            try
            {
                var dto = new NsQuyetDinhFindByIdRequestDto { Id = idQuyetDinh };
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
