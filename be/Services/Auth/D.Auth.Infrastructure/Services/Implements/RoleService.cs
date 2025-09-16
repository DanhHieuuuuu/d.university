using AutoMapper;
using Azure.Core;
using D.Auth.Domain.Dtos.Role;
using D.Auth.Domain.Entities;
using D.Auth.Infrastructure.Services.Abstracts;
using D.ControllerBases.Exceptions;
using D.InfrastructureBase.Service;
using D.InfrastructureBase.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Infrastructure.Services.Implements
{
    public class RoleService : ServiceBase, IRoleService
    {
        public readonly ServiceUnitOfWork _unitOfWork;
        public RoleService(ILogger<RoleService> logger, IHttpContextAccessor contextAccessor, IMapper mapper, ServiceUnitOfWork serviceUnitOfWork) : base(logger, contextAccessor, mapper)
        {
            _unitOfWork = serviceUnitOfWork;
        }

        [Authorize]
        public bool CreateRole(CreateRoleRequestDto request)
        {
            _logger.LogInformation($"{nameof(CreateRole)} method called. Dto: {request}");

            var duplicateKeys = request.RolePermissions
                                    .GroupBy(p => p.PermissonKey.ToLower().Trim())
                                    .Where(g => g.Count() > 1)
                                    .Select(g => g.Key)
                                    .ToList();

            if (duplicateKeys.Any())
            {
                throw new UserFriendlyException(1001, $"Danh sách gửi lên có PermissionKey trùng: {string.Join(", ", duplicateKeys)}");
            }

            var permissionkey = request.RolePermissions.Select(x => x.PermissonKey);

            var checkAny = _unitOfWork.iRolePermissionRepository.TableNoTracking.FirstOrDefault(x => permissionkey.Contains(x.PermissonKey));
            if (checkAny != null)
            {
                throw new UserFriendlyException(1002, $"Đã tồn tại permission key: {checkAny.PermissonKey}");

            }

            var entity = _mapper.Map<Role>(request);

            _unitOfWork.iRoleRepository.Add(entity);
            _unitOfWork.iRoleRepository.SaveChange();
            return true;
        }

        public List<string> GetAllRoleNhanSu()
        {
            _logger.LogInformation($"{nameof(GetAllRoleNhanSu)}.");

            var nsId = CommonUntil.GetCurrentUserId(_contextAccessor);

            var role = _unitOfWork.iUserRoleRepository.TableNoTracking
                        .Where(x => x.NhanSuId == nsId).Include(x => x.Role).ThenInclude(x => x.RolePermissions)
                        .SelectMany(x => x.Role.RolePermissions);

            var listRole = role.Select(x => x.PermissonKey).Distinct().ToList();
            return listRole;
        }

    }
}
