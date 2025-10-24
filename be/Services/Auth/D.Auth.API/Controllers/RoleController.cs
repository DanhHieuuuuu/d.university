using D.Auth.Domain.Dtos.Permission;
using D.Auth.Domain.Dtos.Role;
using D.ControllerBase;
using d.Shared.Permission;
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

        public RoleController(IMediator mediator)
            : base(mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Danh sách role
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("find")]
        public async Task<ResponseAPI> GetAllRole([FromQuery] FindPagingRoleRequestDto dto)
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
        /// Tạo role
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [PermissionFilter(PermissionKeyConstant.Admin)]
        [HttpPost("create-role")]
        public async Task<ResponseAPI> CreateNewRole([FromBody] CreateRoleRequestDto dto)
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
        /// Lấy danh sách permission của tôi
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("my-permissions")]
        public async Task<ResponseAPI> GetAllRoleByMe([FromQuery] GetPermissionNhanSuDto dto)
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
        /// Cập nhật thông tin role
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ResponseAPI> UpdateRoleInformation(
            [FromRoute] int id,
            [FromBody] UpdateRoleDto dto
        )
        {
            try
            {
                var req = new UpdateRoleDto
                {
                    Id = id,
                    Name = dto.Name,
                    Description = dto.Description,
                };
                await _mediator.Send(req);
                return new("Cập nhật thông tin role thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Cập nhật permission cho role
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("{id}/permissions")]
        public async Task<ResponseAPI> UpdateRolePermission(
            [FromRoute] int id,
            [FromBody] UpdateRolePermissionDto dto
        )
        {
            try
            {
                var req = new UpdateRolePermissionDto
                {
                    RoleId = id,
                    PermissionIds = dto.PermissionIds,
                };
                await _mediator.Send(req);
                return new("Đã cập nhật quyền cho role");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Danh sách tất cả các quyền
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("list-permissions")]
        public async Task<ResponseAPI> GetAllPermission([FromQuery] PermissionRequestDto dto)
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
        /// Danh sách tất cả các quyền - dạng cây
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("tree-permissions")]
        public async Task<ResponseAPI> GetAllPermissionTree([FromQuery] PermissionTreeRequestDto dto)
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
        /// Đồng bộ permission
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("fetch-permissions")]
        public async Task<ResponseAPI> FetchImportPermission([FromBody] ImportPermissionCommand dto)
        {
            try
            {
                await _mediator.Send(dto);
                return new("Import permission thành công.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
