using AutoMapper;
using Azure.Core;
using d.Shared.Permission.Error;
using D.Auth.Domain.Dtos.User;
using D.Auth.Domain.Dtos.User.Password;
using D.Auth.Domain.Entities;
using D.Auth.Infrastructure.Services.Abstracts;
using D.ControllerBase.Exceptions;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using D.InfrastructureBase.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Infrastructure.Services.Implements
{
    public class UserService : ServiceBase, IUserService
    {
        private readonly ServiceUnitOfWork _unitOfWork;
        private IConfiguration _configuration;
        public UserService(
                ILogger<UserService> logger,
                IHttpContextAccessor contextAccessor,
                IMapper mapper,
                ServiceUnitOfWork unitOfWork,
                IConfiguration configuration
            ) : base(logger, contextAccessor, mapper)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        public async Task<CreateUserResponseDto> CreateUser(CreateUserRequestDto request)
        {
            _logger.LogInformation($"{nameof(CreateUser)} method called. Dto: {request}");

            var existed = _unitOfWork.iNsNhanSuRepository.FindByMaNhanSu(request.MaNhanSu);
            if (existed == null)
                throw new UserFriendlyException(ErrorCodeConstant.CodeNotFound, "Nhân sự không tồn tại trong hệ thống.");

            if (!string.IsNullOrEmpty(existed.Password))
                throw new UserFriendlyException(ErrorCodeConstant.CodeExits, "Tài khoản đã được tạo trước đó.");


            string rawPassword = string.IsNullOrWhiteSpace(request.Password)
            ? PasswordHelper.GenerateRandomPassword()
            : request.Password;
            var (hash, salt) = PasswordHelper.HashPassword(rawPassword);
            existed.Password = hash;
            existed.PasswordKey = salt;

            _unitOfWork.iNsNhanSuRepository.Update(existed);
            await _unitOfWork.iNsNhanSuRepository.SaveChangeAsync();


            // Gửi email thông báo mật khẩu
            if (!string.IsNullOrWhiteSpace(existed.Email))
            {
                _ = Task.Run(async () =>
                {
                    try
                    {
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Template", "newpassword.html");
                        string body = await System.IO.File.ReadAllTextAsync(filePath);

                        body = body.Replace("{{userName}}", $"{existed.HoDem} {existed.Ten}")
                                   .Replace("{{newPassword}}", rawPassword)
                                   .Replace("{{year}}", DateTime.Now.Year.ToString());

                        var dto = new SendEmailDto
                        {
                            EmailFrom = _configuration["Email_Configuration:Email"],
                            Password = _configuration["Email_Configuration:Password"],
                            Host = _configuration["Email_Configuration:Host"],
                            Post = int.Parse(_configuration["Email_Configuration:Port"]),
                            Title = "Thông tin tài khoản",
                            EmailTo = existed.Email,
                            Body = body
                        };

                        await SendNotification.SendEmail(dto);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Gửi email mật khẩu tạm thất bại cho MaNhanSu={MaNhanSu}", existed.MaNhanSu);
                    }
                });
            }

            return new CreateUserResponseDto
            {
                Id = existed.Id,
                MaNhanSu = existed.MaNhanSu,
                FullName = $"{existed.HoDem} {existed.Ten}",
                Email = existed.Email
            };
        }


        public async Task<bool> UpdateUser(UpdateUserRequestDto request)
        {
            _logger.LogInformation($"{nameof(UpdateUser)} method called. Dto: {request}");

            var existed = _unitOfWork.iNsNhanSuRepository.FindById(request.Id);
            if (existed == null)
                throw new UserFriendlyException(ErrorCodeConstant.CodeNotFound, "Người dùng không tồn tại.");

            if (!string.IsNullOrEmpty(request.NewPassword))
            {
                var (hash, salt) = PasswordHelper.HashPassword(request.NewPassword);
                existed.Password = hash;
                existed.PasswordKey = salt;
            }
            if (!string.IsNullOrEmpty(request.Email))
                existed.Email = request.Email;

            _unitOfWork.iNsNhanSuRepository.Update(existed);

            await _unitOfWork.iUserRepository.SaveChangeAsync();

            return true;
        }


        public bool ChangePassword(ChangePasswordRequestDto request)
        {
            _logger.LogInformation($"{nameof(ChangePassword)} method called. Dto: {request}");

            int userId = CommonUntil.GetCurrentUserId(_contextAccessor);

            NsNhanSu ns = _unitOfWork.iNsNhanSuRepository.FindById(userId);

            if(ns == null)
            {
                return false;
            }

            bool checkPassword = PasswordHelper.VerifyPassword(request.OldPassword, ns.Password, ns.PasswordKey);

            if(!checkPassword)
                throw new UserFriendlyException(ErrorCodeConstant.PasswordWrong, "Sai mật khẩu.");
            
            ns.Password = PasswordHelper.HashPassword(request.NewPassword, ns.PasswordKey);

            _unitOfWork.iNsNhanSuRepository.Update(ns);
            _unitOfWork.iNsNhanSuRepository.SaveChange();
            return true;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordRequestDto request)
        {
            _logger.LogInformation($"{nameof(ResetPasswordAsync)} method called. Dto: {request}");

            NsNhanSu ns = _unitOfWork.iNsNhanSuRepository.FindByMaNhanSuFollowChange(request.MaNhanSu);

            string newPassword = PasswordHelper.GenerateRandomPassword();

            var (hash, salt) = PasswordHelper.HashPassword(newPassword);

            ns.Password = hash;
            ns.PasswordKey = salt;

            _unitOfWork.iNsNhanSuRepository.SaveChange();


            _ = Task.Run(async () => 
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Template", "newpassword.html");
                string body = await System.IO.File.ReadAllTextAsync(filePath);
                body = body.Replace("{{userName}}", $"{ns.HoDem} {ns.Ten}")
                           .Replace("{{newPassword}}", newPassword)
                           .Replace("{{year}}", DateTime.Now.Year.ToString());

                SendEmailDto dto = new SendEmailDto()
                {
                    EmailFrom = _configuration["Email_Configuration:Email"],
                    Password = _configuration["Email_Configuration:Password"],
                    Host = _configuration["Email_Configuration:Host"],
                    Post = int.Parse(_configuration["Email_Configuration:Port"]),
                    Title = "Mật khẩu mới",
                    EmailTo = ns.Email,
                    Body = body
                };
                await SendNotification.SendEmail(dto);
            });

            return true;
        }
    }
}
