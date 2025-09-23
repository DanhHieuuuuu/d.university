using d.Shared.Permission;
using D.Auth.Domain.Dtos.Role;
using D.Auth.Domain.Dtos.User;
using D.ControllerBases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace D.Auth.API.Controllers
{

    [Route("/api/user")]
    [ApiController]
    public class UserController : APIControllerBase
    {
        private IMediator _mediator;
        public UserController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Thêm người dùng
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [PermissionFilter("admin")]
        [HttpPost("create-user")]
        public async Task<ResponseAPI> Login(CreateUserRequestDto dto)
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
