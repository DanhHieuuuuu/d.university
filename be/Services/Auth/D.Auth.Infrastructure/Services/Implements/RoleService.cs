using System.Text.Json;
using AutoMapper;
using D.Auth.Domain.Dtos.Role;
using D.Auth.Domain.Entities;
using D.Auth.Infrastructure.Services.Abstracts;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using D.InfrastructureBase.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace D.Auth.Infrastructure.Services.Implements
{
    public class RoleService : ServiceBase, IRoleService
    {
        public readonly ServiceUnitOfWork _unitOfWork;

        public RoleService(
            ILogger<RoleService> logger,
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            ServiceUnitOfWork serviceUnitOfWork
        )
            : base(logger, contextAccessor, mapper)
        {
            _unitOfWork = serviceUnitOfWork;
        }

        public bool CreateRole(CreateRoleRequestDto request)
        {
            _logger.LogInformation(
                $"{nameof(CreateRole)} method called. Dto: {JsonSerializer.Serialize(request)}"
            );

            var entity = _mapper.Map<Role>(request);

            _unitOfWork.iRoleRepository.Add(entity);
            _unitOfWork.iRoleRepository.SaveChange();

            return true;
        }

        public void UpdateRole(UpdateRoleDto dto)
        {
            _logger.LogInformation(
                $"{nameof(UpdateRole)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var existRole = _unitOfWork.iRoleRepository.TableNoTracking.Any(r => r.Id == dto.Id);
            if (existRole)
            {
                var entity = _mapper.Map<Role>(dto);

                _unitOfWork.iRoleRepository.Update(entity);
                _unitOfWork.iRoleRepository.SaveChange();
            }
            else
            {
                throw new Exception($"RoleId {dto.Id} không tồn tại");
            }
        }

        public void UpdateRolePermission(UpdateRolePermissionDto dto)
        {
            _logger.LogInformation(
                $"{nameof(UpdateRolePermission)} method call. Dto: {JsonSerializer.Serialize(dto)}"
            );

            // Kiểm tra role tồn tại
            var existRole = _unitOfWork.iRoleRepository.TableNoTracking.Any(x =>
                x.Id == dto.RoleId
            );

            if (!existRole)
                throw new Exception("Role không tồn tại");

            var validPermissionIds = _unitOfWork
                .iPermissionRepository.TableNoTracking.Where(p => dto.PermissionIds.Contains(p.Id))
                .Select(p => p.Id)
                .ToList();

            var existingPermissionIds = _unitOfWork
                .iRolePermissionRepository.TableNoTracking.Where(rp => rp.RoleId == dto.RoleId)
                .Select(rp => rp.PermissionId)
                .Where(permissionId =>
                    permissionId.HasValue && validPermissionIds.Contains(permissionId.Value)
                )
                .Select(permissionId => permissionId!.Value)
                .ToList();

            var newPermissionIds = validPermissionIds.Except(existingPermissionIds).ToList();

            if (!newPermissionIds.Any())
                return;

            var newRolePermissions = newPermissionIds
                .Select(id => new RolePermission { RoleId = dto.RoleId, PermissionId = id })
                .ToList();

            _unitOfWork.iRolePermissionRepository.AddRange(newRolePermissions);
            _unitOfWork.iRolePermissionRepository.SaveChange();
        }

        public PageResultDto<RoleResponseDto> GetAllRole(FindPagingRoleRequestDto dto)
        {
            _logger.LogInformation(
                $"{nameof(GetAllRole)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var query =
                from ur in _unitOfWork.iUserRoleRepository.TableNoTracking
                group ur by ur.RoleId into g
                select new { RoleId = g.Key, TotalUser = g.Count() } into A
                join r in _unitOfWork.iRoleRepository.TableNoTracking on A.RoleId equals r.Id
                where
                    (
                        string.IsNullOrEmpty(dto.Keyword)
                        || (
                            !string.IsNullOrEmpty(r.Description)
                            && r.Description.ToLower().Contains(dto.Keyword.ToLower())
                        )
                    )
                select new RoleResponseDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    Status = r.Status,
                    TotalUser = A.TotalUser,
                };

            var totalCount = query.Count();

            var items = query.Skip(dto.SkipCount()).Take(dto.PageSize).ToList();

            return new PageResultDto<RoleResponseDto> { Items = items, TotalItem = totalCount };
        }
    }
}
