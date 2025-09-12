using D.Auth.Infrastructure.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Claims;

namespace d.Shared.Permission
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PermissionFilterAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] _permission;
        private IRoleService _roleService;

        public PermissionFilterAttribute(params string[] permissions)
        {
            _permission = permissions;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _roleService = context.HttpContext.RequestServices.GetRequiredService<IRoleService>();

            // Lấy claims từ token
            var user = context.HttpContext.User;

            if (user?.Identity == null || !user.Identity.IsAuthenticated)
            {
                // Token không hợp lệ hoặc hết hạn → 401
                context.Result = new UnauthorizedObjectResult(new { message = "Token hết hạn hoặc không hợp lệ." });
                return;
            }

            var permissions = _roleService.GetAllRoleNhanSu();

            bool isAuthorize = permissions.Any(p => _permission.Contains(p));

            if (!isAuthorize)
            {
                // Có token nhưng không đủ quyền → 403
                context.Result = new ObjectResult(new { message = "Bạn không có quyền." })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }
        }
    }
}
