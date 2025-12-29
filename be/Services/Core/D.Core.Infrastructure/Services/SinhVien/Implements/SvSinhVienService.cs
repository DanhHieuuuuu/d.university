using AutoMapper;
using AutoMapper.QueryableExtensions;
using d.Shared.Permission.Auth;
using d.Shared.Permission.Error;
using D.ControllerBase.Exceptions;
using D.Core.Domain.Dtos.SinhVien;
using D.Core.Domain.Dtos.SinhVien.Auth;
using D.Core.Domain.Entities.SinhVien;
using D.Core.Infrastructure.Services.SinhVien.Abstracts;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using D.InfrastructureBase.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;


namespace D.Core.Infrastructure.Services.SinhVien.Implements
{
    public class SvSinhVienService : ServiceBase, ISvSinhVienService
    {
        private readonly ServiceUnitOfWork _unitOfWork;
        private IConfiguration _configuration;
        private readonly IDatabase _database;

        public SvSinhVienService(
            ILogger<SvSinhVienService> logger,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork,
            IConfiguration configuration,
            IDatabase database
        )
            : base(logger, httpContext, mapper)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _database = database;
        }

        public PageResultDto<SvSinhVienResponseDto> FindPagingSvSinhVien(SvSinhVienRequestDto dto)
        {
            _logger.LogInformation(
                $"{nameof(FindPagingSvSinhVien)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var query = _unitOfWork.iSvSinhVienRepository.TableNoTracking
            .Where(x => string.IsNullOrEmpty(dto.Keyword)
                     || x.Mssv.Contains(dto.Keyword)
                     || x.Ten.Contains(dto.Keyword));

            if (dto.Khoa.HasValue) query = query.Where(x => x.Khoa == dto.Khoa);
            if (dto.Nganh.HasValue) query = query.Where(x => x.Nganh == dto.Nganh);
            if (dto.KhoaHoc.HasValue) query = query.Where(x => x.KhoaHoc == dto.KhoaHoc);

            var items = query
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .ProjectTo<SvSinhVienResponseDto>(_mapper.ConfigurationProvider)
                .ToList();

            var totalCount = query.Count();

            return new PageResultDto<SvSinhVienResponseDto> { Items = items, TotalItem = totalCount };
        }

        public PageResultDto<SvSinhVienGetAllResponseDto> GetAllSinhVien(SvSinhVienGetAllRequestDto dto)
        {
            _logger.LogInformation(
                $"{nameof(GetAllSinhVien)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var query = _unitOfWork.iSvSinhVienRepository.TableNoTracking;

            if (dto.Khoa.HasValue) query = query.Where(x => x.Khoa == dto.Khoa);
            if (dto.Nganh.HasValue) query = query.Where(x => x.Nganh == dto.Nganh);
            if (dto.KhoaHoc.HasValue) query = query.Where(x => x.KhoaHoc == dto.KhoaHoc);

            var items = query
                .ProjectTo<SvSinhVienGetAllResponseDto>(_mapper.ConfigurationProvider)
                .ToList();

            return new PageResultDto<SvSinhVienGetAllResponseDto>
            {
                Items = items,
                TotalItem = query.Count()
            };
        }


        public async Task<SvSinhVienResponseDto> CreateSinhVien(CreateSinhVienDto dto)
        {
            _logger.LogInformation(
                $"{nameof(CreateSinhVien)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var exist = _unitOfWork.iSvSinhVienRepository.IsSoCccdExits(dto.SoCccd!);

            if (exist)
                throw new Exception("Sinh viên đã tồn tại trong hệ thống (trùng Số CCCD).");

            var newSv = _mapper.Map<SvSinhVien>(dto);

            newSv.Mssv = await _unitOfWork.iSvSinhVienRepository.GenerateMssv(dto.KhoaHoc!.Value);
            newSv.Email2 = _unitOfWork.iSvSinhVienRepository.GenerateEmail(newSv.Mssv!);

            string rawPassword = newSv.NgaySinh?.ToString("ddMMyyyy") ?? newSv.Mssv!;
            
            var (hash, salt) = PasswordHelper.HashPassword(rawPassword);
            newSv.Password = hash;
            newSv.PasswordKey = salt;

            await _unitOfWork.iSvSinhVienRepository.AddAsync(newSv);
            await _unitOfWork.iSvSinhVienRepository.SaveChangeAsync();

            return _mapper.Map<SvSinhVienResponseDto>(newSv);
        }

        public async Task<bool> UpdateSinhVien(UpdateSinhVienDto dto)
        {
            var sv = await _unitOfWork.iSvSinhVienRepository.GetByMssv(dto.Mssv!);
            if (sv == null) throw new Exception("Không tìm thấy sinh viên.");

            _mapper.Map(dto, sv);

            _unitOfWork.iSvSinhVienRepository.Update(sv);
            await _unitOfWork.iSvSinhVienRepository.SaveChangeAsync();
            return true;
        }

        public async Task<bool> DeleteSinhVien(DeleteSinhVienDto dto)
        {
            var sv = await _unitOfWork.iSvSinhVienRepository.GetByMssv(dto.Mssv!);
            if (sv == null) throw new Exception("Không tìm thấy sinh viên.");

            _unitOfWork.iSvSinhVienRepository.Delete(sv);
            await _unitOfWork.iSvSinhVienRepository.SaveChangeAsync();
            return true;
        }

        public async Task<SvSinhVienResponseDto> FindByMssv(FindByMssvDto dto)
        {
            _logger.LogInformation(
                $"{nameof(FindByMssv)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var result = await _unitOfWork.iSvSinhVienRepository.TableNoTracking
                .Where(x => x.Mssv == dto.Mssv)
                .ProjectTo<SvSinhVienResponseDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (result == null)
                throw new Exception($"Không tìm thấy sinh viên có mssv: {dto.Mssv}");

            return result;
        }


        #region auth
        public async Task<SvLoginResponseDto> Login(SvLoginRequestDto loginRequest)
        {
            _logger.LogInformation($"{nameof(Login)} method called. Dto: {JsonSerializer.Serialize(loginRequest)}");

            // 1. Tìm sinh viên theo MSSV
            var sv = await _unitOfWork.iSvSinhVienRepository.GetByMssv(loginRequest.Mssv);

            if (sv == null)
            {
                throw new UserFriendlyException(ErrorCodeConstant.PasswordOrCodeWrong, "Không đúng mật khẩu hoặc tài khoản.");
            }

            // 2. Check mật khẩu
            if (string.IsNullOrEmpty(sv.Password) || string.IsNullOrEmpty(sv.PasswordKey))
            {
                throw new UserFriendlyException(500, "Tài khoản chưa được thiết lập mật khẩu.");
            }

            if (!PasswordHelper.VerifyPassword(loginRequest.Password, sv.Password, sv.PasswordKey))
            {
                throw new UserFriendlyException(ErrorCodeConstant.PasswordWrong, "Mật khẩu không đúng.");
            }

            // 3. Tạo Token
            SvLoginResponseDto result = new SvLoginResponseDto();
            result.User = _mapper.Map<SvSinhVienResponseDto>(sv);

            DateTime date = DateTime.Now;

            result.Token = GenerateToken(sv);
            result.ExpiredToken = date.AddMinutes(int.Parse(_configuration["JwtSettings:ExpiryMinutes"]));

            // Lưu Access Token vào Redis
            await SaveAccessTokenAsync(result.Token, sv.Id, TimeSpan.FromMinutes(int.Parse(_configuration["JwtSettings:ExpiryMinutes"])));

            result.RefreshToken = GenerateRefreshToken();
            result.ExpiredRefreshToken = date.AddDays(7);

            // Lưu Refresh Token vào Redis
            await SaveRefreshTokenAsync(result.Token, result.RefreshToken, TimeSpan.FromDays(7));

            return result;
        }

        public async Task<SvRefreshTokenResponseDto> RefreshToken(SvRefreshTokenRequestDto refreshToken)
        {
            _logger.LogInformation($"{nameof(RefreshToken)} method called.");

            var principal = GetPrincipalFromExpiredToken(refreshToken.Token);
            var claim = principal?.FindFirst(CustomClaimType.UserId);

            if (claim == null) throw new UserFriendlyException(ErrorCode.Unauthorized, "Token không hợp lệ.");

            var checkRefresh = await ValidateRefreshTokenAsync(refreshToken.Token, refreshToken.RefreshToken);
            if (!checkRefresh)
                throw new UserFriendlyException(ErrorCodeConstant.RefreshTokenNotFound, "Refresh token không đúng hoặc đã hết hạn.");

            var sv = _unitOfWork.iSvSinhVienRepository.FindById(int.Parse(claim.Value));

            DateTime date = DateTime.Now;
            string newToken = GenerateToken(sv);
            string newRefreshToken = GenerateRefreshToken();

            await SaveAccessTokenAsync(newToken, sv.Id, TimeSpan.FromMinutes(int.Parse(_configuration["JwtSettings:ExpiryMinutes"])));
            await SaveRefreshTokenAsync(newToken, newRefreshToken, TimeSpan.FromDays(7));

            return new SvRefreshTokenResponseDto
            {
                Token = newToken,
                ExpiredToken = date.AddMinutes(int.Parse(_configuration["JwtSettings:ExpiryMinutes"])),
                RefreshToken = newRefreshToken,
                ExpiredRefreshToken = date.AddDays(7)
            };
        }

        public async Task<bool> Logout(SvLogoutRequestDto logoutRequestDto)
        {
            var token = CommonUntil.GetToken(_contextAccessor);
            await DeleteTokenAsync(token);
            await DeleteTokenAsync(token, true);
            return true;
        }

        private string GenerateToken(SvSinhVien sv)
        {
            var claims = new[]
            {
            new Claim(CustomClaimType.UserId, $"{sv.Id}"),
            new Claim(CustomClaimType.FirstName, sv.HoDem ?? ""),
            new Claim(CustomClaimType.LastName, sv.Ten ?? ""),
            new Claim(CustomClaimType.DateOfBirth, sv.NgaySinh?.ToString() ?? ""),
            // Sinh viên type 4
            new Claim(CustomClaimType.UserType, UserTypeConstant.STUDENT.ToString()), 
            // Thêm MSSV vào claim 
            new Claim("Mssv", sv.Mssv ?? "")
            
        };
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(int.Parse(_configuration["JwtSettings:ExpiryMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

        private async Task SaveAccessTokenAsync(string token, int userId, TimeSpan expiry)
            => await _database.StringSetAsync($"token:{token}", userId.ToString(), expiry);

        private async Task SaveRefreshTokenAsync(string token, string refreshToken, TimeSpan expiry)
            => await _database.StringSetAsync($"refreshToken:{token}", refreshToken, expiry);

        private async Task<bool> ValidateRefreshTokenAsync(string token, string refreshToken)
        {
            string key = $"refreshToken:{token}";
            var value = await _database.StringGetAsync(key);
            return value.HasValue && value.ToString() == refreshToken;
        }

        private async Task DeleteTokenAsync(string token, bool isRefresh = false)
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
                ValidateLifetime = false
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

        #endregion
    }
}
