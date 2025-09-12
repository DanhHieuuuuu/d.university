using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using D.Auth.Domain.Dtos;
using D.Auth.Domain.Dtos.Login;
using D.Auth.Domain.Dtos.UserRole;
using D.Auth.Domain.Entities;
using D.Auth.Infrastructure.Services.Abstracts;
using D.ControllerBases.Exceptions;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using D.InfrastructureBase.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace D.Auth.Infrastructure.Services.Implements
{
    public class NsNhanSuService : ServiceBase, INsNhanSuService
    {
        private readonly ServiceUnitOfWork _unitOfWork;
        private IConfiguration _configuration;

        public NsNhanSuService(
            ILogger<NsNhanSuService> logger,
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork,
            IConfiguration configuration
        )
            : base(logger, contextAccessor, mapper)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public PageResultDto<NsNhanSuResponseDto> FindPagingNsNhanSu(NsNhanSuRequestDto dto)
        {
            _logger.LogInformation($"{nameof(FindPagingNsNhanSu)} method called. Dto: {dto}");

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

        public LoginResponseDto Login(LoginRequestDto loginRequest)
        {
            _logger.LogInformation($"{nameof(Login)} method called. Dto: {loginRequest}");

            var ns = _unitOfWork.iNsNhanSuRepository.TableNoTracking.FirstOrDefault(x =>
                x.MaNhanSu == loginRequest.MaNhanSu
            );

            if (ns == null)
            {
                throw new UserFriendlyException(404, "Không đúng mật khẩu hoặc tài khoản.");
            }

            var result = _mapper.Map<LoginResponseDto>(ns);
            result.Token = GenerateToken(ns);
            result.RefreshToken = GenerateRefreshToken();

            return result;
        }

        public bool AddUserRole(CreateUserRoleDto createUserRole)
        {
            _logger.LogInformation($"{nameof(AddUserRole)} method called. Dto: {createUserRole}");

            var checkAny = _unitOfWork.iUserRoleRepository.TableNoTracking.FirstOrDefault(x =>
                x.RoleId == createUserRole.RoleId && x.NhanSuId == createUserRole.NhanSuId
            );

            if (checkAny != null)
            {
                throw new UserFriendlyException(1005, "Người dùng đã tồn tại quyền này");
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

        public string GenerateToken(NsNhanSu nsNhanSu)
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

        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }
    }
}
