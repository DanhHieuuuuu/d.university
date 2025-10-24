using AutoMapper;
using D.Auth.Domain.Dtos.Permission;
using D.Auth.Domain.Entities;
using D.Auth.Infrastructure.Services.Abstracts;
using D.InfrastructureBase.Service;
using D.InfrastructureBase.Shared;
using d.Shared.Permission.Permission;
using d.Shared.Permission.Role;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace D.Auth.Infrastructure.Services.Implements
{
    public class PermissionService : ServiceBase, IPermissionService
    {
        public readonly ServiceUnitOfWork _unitOfWork;

        public PermissionService(
            ILogger<PermissionService> logger,
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            ServiceUnitOfWork serviceUnitOfWork
        )
            : base(logger, contextAccessor, mapper)
        {
            _unitOfWork = serviceUnitOfWork;
        }

        public async Task ImportPermission(ImportPermissionCommand dto)
        {
            _logger.LogInformation("=== Bắt đầu import dữ liệu Permission ===");

            var permissionDict = PermissionConfig.CoreConfigs;

            // Tạm lưu key -> id
            var keyIdMap = new Dictionary<string, int>();

            // 1️⃣ Thêm các permission không có cha
            foreach (var kv in permissionDict.Values.Where(x => x.ParentKey == null))
            {
                if (
                    await _unitOfWork.iPermissionRepository.TableNoTracking.AnyAsync(p =>
                        p.PermissionKey == kv.PermissonKey
                    )
                )
                    continue;

                var entity = new Permission
                {
                    PermissionKey = kv.PermissonKey,
                    PermissionName = kv.PermissionName,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "system",
                };

                _unitOfWork.iPermissionRepository.Add(entity);
                await _unitOfWork.iPermissionRepository.SaveChangeAsync();
                keyIdMap[kv.PermissonKey] = entity.Id;
            }

            // 2️⃣ Thêm các permission có cha
            foreach (var kv in permissionDict.Values.Where(x => x.ParentKey != null))
            {
                if (
                    await _unitOfWork.iPermissionRepository.TableNoTracking.AnyAsync(p =>
                        p.PermissionKey == kv.PermissonKey
                    )
                )
                    continue;

                if (!keyIdMap.ContainsKey(kv.ParentKey!))
                {
                    _logger.LogWarning(
                        $"Không tìm thấy parent key: {kv.ParentKey} cho permission {kv.PermissonKey}"
                    );
                    continue;
                }

                var entity = new Permission
                {
                    PermissionKey = kv.PermissonKey,
                    PermissionName = kv.PermissionName,
                    ParentID = keyIdMap[kv.ParentKey!],
                };

                _unitOfWork.iPermissionRepository.Add(entity);
            }

            await _unitOfWork.iPermissionRepository.SaveChangeAsync();

            _logger.LogInformation("=== Import Permission hoàn tất ===");
        }

        public List<PermissionResponseDto> GetAllPermission(PermissionRequestDto dto)
        {
            _logger.LogInformation($"{nameof(GetAllPermission)}");

            // Load tất cả quyền 1 lần
            var allPermissions = _unitOfWork
                .iPermissionRepository.TableNoTracking.Select(x => new
                {
                    x.Id,
                    x.PermissionKey,
                    x.PermissionName,
                    x.ParentID,
                })
                .ToList();

            // Tạo dictionary để tra nhanh ParentKey
            var permissionDict = allPermissions.ToDictionary(p => p.Id, p => p.PermissionKey);

            var result = allPermissions
                .Select(p => new PermissionResponseDto
                {
                    Key = p.PermissionKey,
                    Label = p.PermissionName,
                    ParentKey =
                        p.ParentID.HasValue && permissionDict.ContainsKey(p.ParentID.Value)
                            ? permissionDict[p.ParentID.Value]
                            : null,
                })
                .ToList();

            return result;
        }

        public List<string> GetPermissionsByNhanSu(GetPermissionNhanSuDto dto)
        {
            _logger.LogInformation($"{nameof(GetPermissionsByNhanSu)}.");

            var nsId = CommonUntil.GetCurrentUserId(_contextAccessor);

            var roleIds =
                from u in _unitOfWork.iUserRoleRepository.TableNoTracking
                join r in _unitOfWork.iRoleRepository.TableNoTracking on u.RoleId equals r.Id
                where u.NhanSuId == nsId && r.Status == RoleStatus.Active
                select u.RoleId;

            var query =
                from rp in _unitOfWork.iRolePermissionRepository.TableNoTracking
                join rId in roleIds on rp.RoleId equals rId
                join p in _unitOfWork.iPermissionRepository.TableNoTracking
                    on rp.PermissionId equals p.Id
                select p.PermissionKey;

            return query.Distinct().ToList();
        }

        public List<PermissionTreeResponseDto> GetPermissionTree(PermissionTreeRequestDto dto)
        {
            _logger.LogInformation($"{nameof(GetPermissionTree)}");

            // 1) Lấy toàn bộ quyền (chỉ 1 query)
            var permissions = _unitOfWork.iPermissionRepository.TableNoTracking
                .Select(p => new
                {
                    p.Id,
                    p.PermissionKey,
                    p.PermissionName,
                    p.ParentID
                })
                .ToList();

            // 2) Tạo dictionary để build tree nhanh
            var dict = permissions.ToDictionary(
                p => p.Id,
                p => new PermissionTreeResponseDto
                {
                    Key = p.PermissionKey,
                    Label = p.PermissionName,
                    ParentKey = null
                });

            // 3) Nối node con vào node cha
            foreach (var p in permissions)
            {
                if (p.ParentID.HasValue && dict.TryGetValue(p.ParentID.Value, out var parentNode))
                {
                    var childNode = dict[p.Id];
                    childNode.ParentKey = parentNode.Key;
                    parentNode.Children.Add(childNode);
                }
            }

            // 4) Lấy danh sách node gốc (những quyền không có cha)
            var result = permissions
                .Where(p => p.ParentID == null)
                .Select(p => dict[p.Id])
                .ToList();

            return result;
        }
    }
}
