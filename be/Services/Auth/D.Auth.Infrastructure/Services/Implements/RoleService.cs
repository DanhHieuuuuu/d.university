using System.Text.Json;
using AutoMapper;
using D.Auth.Domain.Dtos.Role;
using D.Auth.Domain.Entities;
using D.Auth.Infrastructure.Services.Abstracts;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
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

            var existRole = _unitOfWork.iRoleRepository.TableNoTracking.FirstOrDefault(r =>
                r.Id == dto.Id
            );

            if (existRole != null)
            {
                existRole.Name = dto.Name;
                existRole.Description = dto.Description;

                _unitOfWork.iRoleRepository.Update(existRole);
                _unitOfWork.iRoleRepository.SaveChange();
            }
            else
            {
                throw new Exception($"RoleId {dto.Id} không tồn tại");
            }
        }

        public void DeleteRole(int id)
        {
            _logger.LogInformation($"{nameof(DeleteRole)} method called. RoleId: {id}");

            var existRole = _unitOfWork.iRoleRepository.TableNoTracking.FirstOrDefault(r =>
                r.Id == id
            );

            if (existRole != null)
            {
                var removeUsers = _unitOfWork.iUserRoleRepository.TableNoTracking.Where(ur =>
                    ur.RoleId == id
                );
                var removePermission = _unitOfWork.iRolePermissionRepository.TableNoTracking.Where(
                    rp => rp.RoleId == id
                );

                _unitOfWork.iRoleRepository.Delete(existRole);
                _unitOfWork.iUserRoleRepository.DeleteRange(removeUsers);
                _unitOfWork.iRolePermissionRepository.DeleteRange(removePermission);
                _unitOfWork.iRoleRepository.SaveChange();
            }
            else
            {
                throw new Exception($"RoleId: {id} không tồn tại hoặc đã bị xóa");
            }
        }

        public PageResultDto<RoleResponseDto> GetAllRole(FindPagingRoleRequestDto dto)
        {
            _logger.LogInformation(
                $"{nameof(GetAllRole)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            //var query =
            //    from r in _unitOfWork.iRoleRepository.TableNoTracking
            //    join u in _unitOfWork.iUserRoleRepository.TableNoTracking
            //        on r.Id equals u.RoleId
            //        into grp
            //    from x in grp.DefaultIfEmpty()
            //    group x by new
            //    {
            //        r.Id,
            //        r.Name,
            //        r.Description,
            //        r.Status,
            //    } into g
            //    select new RoleResponseDto
            //    {
            //        Id = g.Key.Id,
            //        Name = g.Key.Name,
            //        Description = g.Key.Description,
            //        Status = g.Key.Status,
            //        TotalUser = g.Count(x => x != null),
            //    };

            var query = _unitOfWork.iRoleRepository.TableNoTracking.Select(r => new RoleResponseDto
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                Status = r.Status,
                TotalUser = r.UserRoles.Count(),
            });

            if (!string.IsNullOrEmpty(dto.Keyword))
            {
                query = query.Where(x =>
                    string.IsNullOrEmpty(dto.Keyword)
                    || (
                        !string.IsNullOrEmpty(x.Description)
                        && x.Description.ToLower().Contains(dto.Keyword.ToLower())
                    )
                    || (
                        !string.IsNullOrEmpty(x.Name)
                        && x.Name.ToLower().Contains(dto.Keyword.ToLower())
                    )
                );
            }

            var totalCount = query.Count();

            var items = query.Skip(dto.SkipCount()).Take(dto.PageSize).ToList();

            return new PageResultDto<RoleResponseDto> { Items = items, TotalItem = totalCount };
        }

        public RoleFindByIdResponseDto FindRoleById(int id)
        {
            _logger.LogInformation($"{nameof(FindRoleById)} method called. RoleId = {id}");

            var exist = _unitOfWork.iRoleRepository.TableNoTracking.FirstOrDefault(r => r.Id == id);

            if (exist != null)
            {
                var query =
                    from rp in _unitOfWork.iRolePermissionRepository.TableNoTracking
                    join p in _unitOfWork.iPermissionRepository.TableNoTracking
                        on rp.PermissionId equals p.Id
                    where rp.RoleId == id
                    select new { p.Id, p.PermissionKey };

                var result = new RoleFindByIdResponseDto
                {
                    Id = id,
                    Name = exist.Name,
                    Description = exist.Description,
                    Permissions = query.Select(p => p.PermissionKey).Distinct().ToList(),
                    PermissionIds = query.Select(p => p.Id).Distinct().ToList(),
                };

                return result;
            }
            else
            {
                throw new Exception($"RoleId không tồn tại hoặc đã bị xóa");
            }
        }

        public void UpdateRolePermission(UpdateRolePermissionDto dto)
        {
            _logger.LogInformation(
                $"{nameof(UpdateRolePermission)} method call. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var existRole = _unitOfWork.iRoleRepository.TableNoTracking.Any(x =>
                x.Id == dto.RoleId
            );
            if (!existRole)
                throw new Exception("Role không tồn tại");

            // Lấy permission hợp lệ từ DTO
            var validPermissionIds = _unitOfWork
                .iPermissionRepository.TableNoTracking.Where(p => dto.PermissionIds.Contains(p.Id))
                .Select(p => p.Id)
                .ToList();

            // Lấy permission hiện tại của role
            var currentPermissionIds = _unitOfWork
                .iRolePermissionRepository.TableNoTracking.Where(rp =>
                    rp.RoleId == dto.RoleId && rp.PermissionId.HasValue
                )
                .Select(rp => rp.PermissionId!.Value)
                .ToList();

            // Add những permission mới
            var addPermissionIds = validPermissionIds.Except(currentPermissionIds).ToList();

            // Xoá hẳn những permission không còn trong DTO
            var removePermissionIds = currentPermissionIds.Except(validPermissionIds).ToList();

            if (addPermissionIds.Any())
            {
                var newRolePermissions = addPermissionIds
                    .Select(id => new RolePermission { RoleId = dto.RoleId, PermissionId = id })
                    .ToList();

                _unitOfWork.iRolePermissionRepository.AddRange(newRolePermissions);
            }

            if (removePermissionIds.Any())
            {
                var removeEntities = _unitOfWork
                    .iRolePermissionRepository.Table.Where(rp =>
                        rp.RoleId == dto.RoleId
                        && removePermissionIds.Contains(rp.PermissionId!.Value)
                    )
                    .ToList();

                _unitOfWork.iRolePermissionRepository.RemoveRange(removeEntities);
            }

            _unitOfWork.iRolePermissionRepository.SaveChange();
        }
    }
}
