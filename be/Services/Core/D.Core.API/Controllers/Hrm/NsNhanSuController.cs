using d.Shared.Permission;
using D.ControllerBases;
using D.Core.Domain.Dtos.Hrm;
using D.Core.Domain.Dtos.Hrm.NhanSu;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.API.Controllers.Hrm
{
    [Route("/api/nhansu")]
    [ApiController]
    public class NsNhanSuController : APIControllerBase
    {
        private IMediator _mediator;

        public NsNhanSuController(IMediator mediator)
            : base(mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Danh sách nhân sự
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("find")]
        public async Task<ResponseAPI> GetListNhanSu(NsNhanSuRequestDto dto)
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
        /// Thêm mới nhân sự
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<ResponseAPI> CreateNhanSu(CreateNhanSuDto dto)
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
        /// Tạo hợp đồng
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [PermissionFilter(PermissionKeyConstant.Admin)]
        [HttpPost("create-hd")]
        public async Task<ResponseAPI> CreateHopDong(CreateHopDongDto dto)
        {
            try
            {
                await _mediator.Send(dto);
                return new();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
