using d.Shared.Permission;
using D.Auth.Domain.Dtos;
using D.Auth.Domain.Dtos.Login;
using D.Auth.Domain.Dtos.UserRole;
using D.ControllerBase;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace D.Auth.API.Controllers
{
    [Route("/api/nhansu")]
    [ApiController]
    public class NsNhanSuController : APIControllerBase
    {
        private IMediator _mediator;
        public NsNhanSuController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("list-nhan-su")]
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
        /// Đăng nhập
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ResponseAPI> Login(LoginRequestDto dto)
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
        /// Đăng xuất
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        //[PermissionFilter(PermissionKeyConstant.Admin)]
        [HttpGet("logout")]
        public async Task<ResponseAPI> Logout([FromQuery] LogoutRequestDto dto)
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
        /// Refresh token
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("refresh-token")]
        public async Task<ResponseAPI> RefreshToken(RefreshTokenRequestDto dto)
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
        /// Thêm người dùng vào role
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [PermissionFilter(PermissionKeyConstant.Admin)]
        [HttpPost("create-user-role")]
        public async Task<ResponseAPI> CreateUserRole(CreateUserRoleDto dto)
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
