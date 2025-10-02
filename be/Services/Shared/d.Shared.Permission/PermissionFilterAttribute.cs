using D.ControllerBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace d.Shared.Permission
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PermissionFilterAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly string[] _permission;
        //private INsNhanSuService _nsNhanSuService;

        public PermissionFilterAttribute(params string[] permissions)
        {
            _permission = permissions;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var httpClient = new HttpClient();
            var token = context.HttpContext.Request.Headers["Authorization"].ToString();

            httpClient.DefaultRequestHeaders.Add("Authorization", token);

            // Gọi sang service Auth để check quyền
            var response = await httpClient.GetAsync("http://localhost:5268/api/role/get-all-role");

            if((int)response.StatusCode == 401)
            {
                context.Result = new UnauthorizedObjectResult(new { message = "Token hết hạn." });
                return;
            }

            var permissions = await response.Content.ReadFromJsonAsync<ResponseAPI<List<string>>>();
            var list = permissions?.Data ?? new List<string>();

            var _database = context.HttpContext.RequestServices.GetRequiredService<IDatabase>();

            string key = $"token:{token.Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase).Trim()}";
            var checkToken = await _database.KeyExistsAsync(key);
            if (!checkToken)
            {
                context.Result = new UnauthorizedObjectResult(new { message = "Token hết hạn hoặc không hợp lệ." });
                return;
            }

            bool isAuthorize = list.Any(p => _permission.Contains(p));

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
