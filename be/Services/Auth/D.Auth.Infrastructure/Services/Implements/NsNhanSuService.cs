using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using d.Shared.Permission.Error;
using D.Auth.Domain.Dtos;
using D.Auth.Domain.Dtos.Login;
using D.Auth.Domain.Dtos.UserRole;
using D.Auth.Domain.Entities;
using D.Auth.Infrastructure.Services.Abstracts;
using D.ControllerBase.Exceptions;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using D.InfrastructureBase.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;

namespace D.Auth.Infrastructure.Services.Implements
{
    public class NsNhanSuService : ServiceBase, INsNhanSuService
    {
        private readonly ServiceUnitOfWork _unitOfWork;
        private IConfiguration _configuration;
        private readonly IDatabase _database;
        public NsNhanSuService(
            ILogger<NsNhanSuService> logger,
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork,
            IConfiguration configuration,
            IDatabase database
        )
            : base(logger, contextAccessor, mapper)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _database = database;
        }

        public PageResultDto<NsNhanSuResponseDto> FindPagingNsNhanSu(NsNhanSuRequestDto dto)
        {
            _logger.LogInformation($"{nameof(FindPagingNsNhanSu)} method called. Dto: {JsonSerializer.Serialize(dto)}");

            var query = _unitOfWork.iNsNhanSuRepository.TableNoTracking.Where(x =>
                string.IsNullOrEmpty(dto.Keyword) || dto.Keyword == x.MaSoThue
            );
            var result = _mapper.Map<List<NsNhanSuResponseDto>>(query);

            return new PageResultDto<NsNhanSuResponseDto>
            {
                Items = result.Skip(dto.SkipCount()).Take(dto.PageSize).ToList(),
                TotalItem = result.Count(),
            };
        }

        // Đăng nhập

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequest)
        {
            _logger.LogInformation($"{nameof(Login)} method called. Dto: {JsonSerializer.Serialize(loginRequest)}");

            var ns = _unitOfWork.iNsNhanSuRepository.FindByMaNhanSu(loginRequest.MaNhanSu);

            if (ns == null)
            {
                throw new UserFriendlyException(ErrorCodeConstant.PasswordOrCodeWrong, "Không đúng mật khẩu hoặc tài khoản.");
            }

            if (string.IsNullOrEmpty(ns.Password) || string.IsNullOrEmpty(ns.PasswordKey))
            {
                throw new UserFriendlyException(500, "Tài khoản chưa được thiết lập mật khẩu.");
            }
            if (!PasswordHelper.VerifyPassword(loginRequest.Password, ns.Password, ns.PasswordKey))
            {
                throw new UserFriendlyException(ErrorCodeConstant.PasswordWrong, "Mật khẩu không đúng.");
            }

            LoginResponseDto result = new LoginResponseDto();
            result.User = _mapper.Map<NsNhanSuResponseDto>(ns);

            DateTime date = DateTime.Now;

            result.Token = GenerateToken(ns);
            result.ExpiredToken = date.AddMinutes(int.Parse(_configuration["JwtSettings:ExpiryMinutes"]));
            await SaveAccessTokenAsync(result.Token, ns.Id, TimeSpan.FromMinutes(int.Parse(_configuration["JwtSettings:ExpiryMinutes"])));
            
            result.RefreshToken = GenerateRefreshToken();
            result.ExpiredRefreshToken = date.AddDays(7);
            await SaveRefreshTokenAsync(result.Token, result.RefreshToken, TimeSpan.FromDays(7));

            return result;
        }



        public bool AddUserRole(CreateUserRoleDto createUserRole)
        {
            _logger.LogInformation($"{nameof(AddUserRole)} method called. Dto: {JsonSerializer.Serialize(createUserRole)}");

            var checkAny = _unitOfWork.iUserRoleRepository.FindByRoleIdAndNsId(createUserRole.RoleId, createUserRole.NhanSuId);

            if (checkAny != null)
            {
                throw new UserFriendlyException(ErrorCodeConstant.UserExists, "Người dùng đã tồn tại quyền này");
            }
            _unitOfWork.iUserRoleRepository.Add(
                new UserRole()
                {
                    RoleId = createUserRole.RoleId,
                    NhanSuId = createUserRole.NhanSuId,
                }
            );
            _unitOfWork.iUserRoleRepository.SaveChange();
            return true;
        }

        public async Task<RefreshTokenResponseDto> RefreshToken(RefreshTokenRequestDto refreshToken)
        {
            _logger.LogInformation($"{nameof(RefreshToken)} method called. Dto: {JsonSerializer.Serialize(refreshToken)}");

            var principal = GetPrincipalFromExpiredToken(refreshToken.Token);

            var claim = principal?.FindFirst(CustomClaimType.UserId);

            if (claim == null)
            {
                throw new UserFriendlyException(ErrorCode.Unauthorized, "Token không hợp lệ.")
;           }

            var checkRefresh = await ValidateRefreshTokenAsync(refreshToken.Token, refreshToken.RefreshToken);

            if (!checkRefresh)
                throw new UserFriendlyException(ErrorCodeConstant.RefreshTokenNotFound, "Refresh token không đúng hoặc đã hết hạn.");
            var ns = _unitOfWork.iNsNhanSuRepository.FindById(int.Parse(claim.Value));

            DateTime date = DateTime.Now;

            string newToken = GenerateToken(ns);
            string newRefreshToken = GenerateRefreshToken();

            await SaveAccessTokenAsync(newToken, ns.Id, TimeSpan.FromMinutes(int.Parse(_configuration["JwtSettings:ExpiryMinutes"])));
            await SaveRefreshTokenAsync(newToken, newRefreshToken, TimeSpan.FromDays(7));

            return new RefreshTokenResponseDto()
            {
                Token = newToken,
                ExpiredToken = date.AddMinutes(int.Parse(_configuration["JwtSettings:ExpiryMinutes"])),
                RefreshToken = newRefreshToken,
                ExpiredRefreshToken = date.AddDays(7)
            };
        }

        public async Task<bool> Logout(LogoutRequestDto logoutRequestDto)
        {
            _logger.LogInformation($"{nameof(Logout)} method called. Dto: {JsonSerializer.Serialize(logoutRequestDto)}");

            var token = CommonUntil.GetToken(_contextAccessor);

            await DeleteTokenAsync(token);
            await DeleteTokenAsync(token, true);

            return true;
        }

        // Lưu access token
        private async Task SaveAccessTokenAsync(string token, int userId, TimeSpan expiry)
        {
            await _database.StringSetAsync($"token:{token}", userId.ToString(), expiry);
        }

        // Lưu refresh token
        public async Task SaveRefreshTokenAsync(string token, string refreshToken, TimeSpan expiry)
        {
            await _database.StringSetAsync($"refreshToken:{token}", refreshToken, expiry);
        }

        // Kiểm tra token hợp lệ
        public async Task<bool> ValidateTokenAsync()
        {
            var token = CommonUntil.GetToken(_contextAccessor);
            string key =  $"token:{token}";
            return await _database.KeyExistsAsync(key);
        }


        // Kiểm tra refresh token hợp lệ
        public async Task<bool> ValidateRefreshTokenAsync(string token, string refreshToken)
        {
            string key = $"refreshToken:{token}";
            var value = await _database.StringGetAsync(key);

            return value.HasValue && value.ToString() == refreshToken;
        }


        // Xóa token (logout)
        public async Task DeleteTokenAsync(string token, bool isRefresh = false)
        {
            string key = isRefresh ? $"refreshToken:{token}" : $"token:{token}";
            await _database.KeyDeleteAsync(key);
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"])),
                ValidateLifetime = false // ⚠️ Bỏ qua thời hạn để lấy được Claims
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        private string GenerateToken(NsNhanSu nsNhanSu)
        {
            var claims = new[]
            {
                new Claim(CustomClaimType.UserId, $"{nsNhanSu.Id}"),
                new Claim(CustomClaimType.FirstName, nsNhanSu.HoDem),
                new Claim(CustomClaimType.LastName, nsNhanSu.Ten),
                new Claim(CustomClaimType.DateOfBirth, nsNhanSu.NgaySinh.ToString()),
            };
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"])
            );
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                    int.Parse(_configuration["JwtSettings:ExpiryMinutes"])
                ),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }
    }
}
