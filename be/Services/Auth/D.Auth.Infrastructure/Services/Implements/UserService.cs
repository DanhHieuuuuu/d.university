using AutoMapper;
using D.Auth.Domain.Dtos.User;
using D.Auth.Domain.Entities;
using D.Auth.Infrastructure.Services.Abstracts;
using D.ControllerBases.Exceptions;
using D.InfrastructureBase.Service;
using Microsoft.AspNetCore.Http;
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
        private readonly IPasswordService _passwordService;
        public UserService(
                ILogger<UserService> logger,
                IHttpContextAccessor contextAccessor,
                IMapper mapper,
                ServiceUnitOfWork unitOfWork,
                IPasswordService passwordService
            ) : base(logger, contextAccessor, mapper)
        {
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
        }
        public async Task<CreateUserResponseDto> CreateUser(CreateUserRequestDto request)
        {
            _logger.LogInformation($"{nameof(CreateUser)} method called. Dto: {request}");

            var existed = _unitOfWork.iNsNhanSuRepository.TableNoTracking
                .FirstOrDefault(x => x.MaNhanSu == request.MaNhanSu);
            if (existed != null)
            {
                throw new UserFriendlyException(1006, "Mã nhân sự đã tồn tại.");
            }

            var entity = _mapper.Map<NsNhanSu>(request);

            // Mật khẩu mặc định = ngày sinh (ddMMyyyy)
            var rawPassword = request.NgaySinh.ToString("ddMMyyyy");
            var (hash, salt) = _passwordService.HashPassword(rawPassword);
            entity.Password = hash;
            entity.PasswordKey = salt;


            _unitOfWork.iNsNhanSuRepository.Add(entity);
            await _unitOfWork.iUserRepository.SaveChangeAsync();

            return new CreateUserResponseDto
            {
                Id = entity.Id,
                MaNhanSu = entity.MaNhanSu,
                FullName = $"{entity.HoDem} {entity.Ten}",
                Email = entity.Email
            };
        }

    }
}
