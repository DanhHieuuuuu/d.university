using d.Shared.Permission;
using d.Shared.Permission.Permission;
using D.Auth.Domain.Dtos.Role;
using D.Auth.Domain.Dtos.User;
using D.Auth.Domain.Dtos.User.Password;
using D.Auth.Domain.Dtos.UserRole;
using D.ControllerBase;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.IO;

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
            [PermissionFilter(PermissionKeys.UserButtonAccountManagerAdd)]

            [HttpPost("create-user")]
            public async Task<ResponseAPI> CreateUser([FromBody] CreateUserRequestDto dto)
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
        /// Thay đổi người dùng
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [PermissionFilter(PermissionKeys.UserButtonAccountManagerUpdate)]
        [HttpPut("update-user")]
        public async Task<ResponseAPI> UpdateUser(UpdateUserRequestDto dto)
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
        [PermissionFilter(PermissionKeys.UserButtonAccountManagerUpdate)]
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

        /// <summary>
        /// Cập nhật ảnh đại diện
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        //[PermissionFilter(PermissionKeyConstant.Admin)]
        [HttpPut("update-image-user")]
        public async Task<IActionResult> UpdateImageUser(UpdateImageUserDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return FileByStream(result, dto.File.FileName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update nhóm quyền cho User
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [PermissionFilter(PermissionKeys.UserButtonAccountManagerUpdatePermission)]
        [HttpPost("update-roles-to-user")]
        public async Task<ResponseAPI> UpdateRoleToUser(UpdateUserRoleDto dto)
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
        /// Thông tin của Role của User
        /// </summary>
        /// <param name="nhanSuId"></param>
        /// <returns></returns>
        [HttpGet("{nhanSuId}")]
        public async Task<ResponseAPI> GetUserRolesByUserId([FromRoute] int nhanSuId)
        {
            try
            {
                var req = new GetUserRolesByUserIdRequestDto { NhanSuId = nhanSuId };
                var result = await _mediator.Send(req);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Vô hiệu hóa hoặc kích hoạt trạng thái tài khoản
        /// </summary>
        /// <param name="nhanSuId"></param>
        /// <returns></returns>
        [PermissionFilter(PermissionKeys.UserButtonAccountManagerLock)]
        [HttpPost("{nhanSuId}/change-status")]
        public async Task<ResponseAPI> ChangeStatusUser([FromRoute] int nhanSuId)
        {
            try
            {
                var req = new ChangeStatusUserDto { NhanSuId = nhanSuId };
                var result = await _mediator.Send(req);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
