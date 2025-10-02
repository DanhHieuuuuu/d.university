using d.Shared.Permission;
using D.Auth.Domain.Dtos.Role;
using D.Auth.Domain.Dtos.User;
using D.Auth.Domain.Dtos.User.Password;
using D.ControllerBase;
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
        [PermissionFilter(PermissionKeyConstant.Admin)]
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
        /// <summary>
        /// Thay đổi mật khẩu
        /// </summary>
        /// <param name="dto">request</param>
        /// <returns></returns>
        [PermissionFilter(PermissionKeyConstant.Admin)]
        [HttpPost("change-password")]
        public async Task<ResponseAPI> ChangePassword(ChangePasswordRequestDto dto)
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
        /// Lấy lại mật khẩu
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("reset-password")]
        public async Task<ResponseAPI> ResetPassword(ResetPasswordRequestDto dto)
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
