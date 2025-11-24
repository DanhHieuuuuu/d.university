using AutoMapper;
using d.Shared.Permission.Auth;
using d.Shared.Permission.Permission;
using d.Shared.Permission.Role;
using D.Auth.Domain.Dtos.Permission;
using D.Auth.Domain.Entities;
using D.Auth.Infrastructure.Services.Abstracts;
using D.InfrastructureBase.Service;
using D.InfrastructureBase.Shared;
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
            _logger.LogInformation("Bắt đầu import dữ liệu Permission");

            var permissionDict = PermissionConfig.CoreConfigs;
            var keyIdMap = new Dictionary<string, int>();

            // Load tất cả permission hiện có trong DB
            var existing = await _unitOfWork.iPermissionRepository.TableNoTracking.ToListAsync();
            foreach (var ex in existing)
                keyIdMap[ex.PermissionKey] = ex.Id;

            // 2Tách permission có cha và không có cha
            var noParent = permissionDict.Values.Where(x => x.ParentKey == null).ToList();
            var withParent = permissionDict.Values.Where(x => x.ParentKey != null).ToList();

            // Thêm hoặc cập nhật permission 
            foreach (var kv in noParent)
            {
                if (keyIdMap.TryGetValue(kv.PermissonKey, out var id))
                {
                    // Đã tồn tại -> kiểm tra xem ParentID có khác null không
                    var entity = existing.First(e => e.Id == id);
                    if (entity.ParentID != null)
                    {
                        entity.ParentID = null;
                        _unitOfWork.iPermissionRepository.Update(entity);
                        await _unitOfWork.iPermissionRepository.SaveChangeAsync();
                    }
                    continue;
                }

                // Thêm mới
                var newEntity = new Permission
                {
                    PermissionKey = kv.PermissonKey,
                    PermissionName = kv.PermissionName,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "system",
                };
                _unitOfWork.iPermissionRepository.Add(newEntity);
                await _unitOfWork.iPermissionRepository.SaveChangeAsync();
                keyIdMap[kv.PermissonKey] = newEntity.Id;
                existing.Add(newEntity);
            }

            // 4Thêm hoặc cập nhật permission có cha
            var remain = withParent.ToList();
            while (remain.Any())
            {
                bool added = false;

                foreach (var kv in remain.ToList())
                {
                    if (!keyIdMap.ContainsKey(kv.ParentKey!))
                        continue; 

                    if (keyIdMap.TryGetValue(kv.PermissonKey, out var id))
                    {
                        // Đã tồn tại -> cập nhật ParentID 
                        var entity = existing.First(e => e.Id == id);
                        var newParentId = keyIdMap[kv.ParentKey!];
                        if (entity.ParentID != newParentId)
                        {
                            entity.ParentID = newParentId;
                            _unitOfWork.iPermissionRepository.Update(entity);
                            await _unitOfWork.iPermissionRepository.SaveChangeAsync();
                        }
                        remain.Remove(kv);
                        added = true;
                        continue;
                    }

                    // Thêm mới
                    var newEntity = new Permission
                    {
                        PermissionKey = kv.PermissonKey,
                        PermissionName = kv.PermissionName,
                        ParentID = keyIdMap[kv.ParentKey!],
                        CreatedDate = DateTime.Now,
                        CreatedBy = "system",
                    };
                    _unitOfWork.iPermissionRepository.Add(newEntity);
                    await _unitOfWork.iPermissionRepository.SaveChangeAsync();

                    keyIdMap[kv.PermissonKey] = newEntity.Id;
                    existing.Add(newEntity);

                    remain.Remove(kv);
                    added = true;
                }

                if (!added)
                {
                    _logger.LogError("Có permission không import được do thiếu parent!");
                    foreach (var r in remain)
                        _logger.LogError($" - Permission lỗi: {r.PermissonKey}, Parent: {r.ParentKey}");
                    throw new Exception("Không thể import permission do thiếu ParentID.");
                }
            }

            _logger.LogInformation("Import Permission hoàn tất");
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
            var userType = CommonUntil.GetCurrentUserType(_contextAccessor);

            if (userType == UserTypeConstant.SUPER_ADMIN)
            {
                // trả về tất cả permission
                var allPermissions = _unitOfWork.iPermissionRepository
                    .TableNoTracking
                    .Select(p => p.PermissionKey)
                    .ToList();
                return allPermissions;
            }

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
            var permissions = _unitOfWork
                .iPermissionRepository.TableNoTracking.Select(p => new
                {
                    p.Id,
                    p.PermissionKey,
                    p.PermissionName,
                    p.ParentID,
                })
                .ToList();

            // 2) Tạo dictionary để build tree nhanh
            var dict = permissions.ToDictionary(
                p => p.Id,
                p => new PermissionTreeResponseDto
                {
                    Id = p.Id,
                    Key = p.PermissionKey,
                    Label = p.PermissionName,
                }
            );

            // 3) Nối node con vào node cha
            foreach (var p in permissions)
            {
                if (p.ParentID.HasValue && dict.TryGetValue(p.ParentID.Value, out var parentNode))
                {
                    var childNode = dict[p.Id];
                    childNode.Id = p.Id;
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
