using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace D.InfrastructureBase.Shared
{
    public class CommonUntil
    {
        public static int GetCurrentUserId(IHttpContextAccessor httpContextAccessor)
        {
            //var claims = httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity;
            ////nếu trong program dùng JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            ////thì các claim type sẽ không bị ghi đè tên nên phải dùng trực tiếp "sub"
            //var claim = claims?.FindFirst(CustomClaimType.UserId);
            //if (claim == null)
            //{
            //    throw new Exception($"Token không chứa claim \"{CustomClaimType.UserId}\"");
            //}

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(GetToken(httpContextAccessor));
            var userIdClaim = jwt.Claims.FirstOrDefault(c => c.Type == CustomClaimType.UserId);
            if (userIdClaim == null)
                throw new Exception($"Token không chứa claim \"{CustomClaimType.UserId}\"");


            return int.Parse(userIdClaim.Value);

            //return int.Parse(claim.Value);
        }
        public static string GetToken(IHttpContextAccessor httpContextAccessor)
        {
            var authorizationHeader = httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                throw new Exception("Không tìm thấy token trong header Authorization.");
            }

            // Cắt bỏ "Bearer " để lấy phần token thực sự
            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            return token;
        }

    }
}
