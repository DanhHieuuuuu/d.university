using d.Shared.Permission;
using D.Auth.Domain.Dtos.Role;
using D.ControllerBase;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace D.Auth.API.Controllers
{
    [Route("/api/role")]
    [ApiController]
    public class RoleController : APIControllerBase
    {
        private IMediator _mediator;
        public RoleController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Tạo role
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [PermissionFilter(PermissionKeyConstant.Admin)]
        [HttpPost("create-role")]
        public async Task<ResponseAPI> Login(CreateRoleRequestDto dto)
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
        /// Lấy danh sách role
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("get-all-role")]
        public async Task<ResponseAPI> GetAllRole([FromQuery] RoleRequestDto dto)
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
