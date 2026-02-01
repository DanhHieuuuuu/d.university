using AutoMapper;
using d.Shared.Permission.Auth;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming.Paging;
using D.Core.Domain.Entities.Delegation.Incoming;
using D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using D.InfrastructureBase.Shared;
using D.Notification.ApplicationService.Abstracts;
using D.Notification.Domain.Enums;
using D.Notification.Dtos;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Services.Delegation.Incoming.Implements
{
    public class DepartmentSupportService : ServiceBase, IDepartmentSupportService
    {
        private readonly ServiceUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;

        public DepartmentSupportService(
            ILogger<DepartmentSupportService> logger,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork,
            INotificationService notificationService
        )
            : base(logger, httpContext, mapper)
        {
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
        }

        public async Task<List<CreateDepartmentSupportResponseDto>> CreateDepartmentSupport(CreateDepartmentSupportRequestDto dto)
        {
            _logger.LogInformation(
                $"{nameof(CreateDepartmentSupport)} method called, dto: {JsonSerializer.Serialize(dto)}."
            );

            var userId = CommonUntil.GetCurrentUserId(_contextAccessor);
            var delegation = _unitOfWork.iDelegationIncomingRepository.FindById(dto.DelegationIncomingId);
            //Kiểm tra tất cả phòng ban có tồn tại không
            var phongBanIds = dto.DepartmentSupportIds.Distinct().ToList();

            var phongBanExistCount = await _unitOfWork.iDmPhongBanRepository
                .TableNoTracking
                .CountAsync(x => phongBanIds.Contains(x.Id) && !x.Deleted);

            if (phongBanExistCount != phongBanIds.Count)
                throw new Exception("Danh sách phòng ban hỗ trợ có phòng ban không tồn tại");
            
            foreach (var item in phongBanIds)
            {
                var data = _unitOfWork.iDepartmentSupportRepository.TableNoTracking.Where(x => x.DepartmentSupportId == item && x.DelegationIncomingId == dto.DelegationIncomingId);
                if(data.Count() > 0)
                {
                    var department = _unitOfWork.iDmPhongBanRepository.FindById(item);
                    throw new Exception($"Phòng ban {department.TenPhongBan} đã hỗ trợ đoàn {delegation.Name}");
                }

            }


            //Map danh sách entity
            var newSupporters = phongBanIds.Select(id => new DepartmentSupport
            {
                DepartmentSupportId = id,
                // map các field khác nếu có
                CreatedBy = userId.ToString(),
                CreatedDate = DateTime.Now,
                Content = dto.Content,
                DelegationIncomingId = dto.DelegationIncomingId
            }).ToList();

            //Add range
            _unitOfWork.iDepartmentSupportRepository.AddRange(newSupporters);
            await _unitOfWork.SaveChangesAsync();

            //Gửi thông báo
            await _notificationService.SendAsync(new NotificationMessage
            {
                Receiver = new Receiver
                {
                    UserId = delegation.IdStaffReception,
                },
                Title = "Phân công hỗ trợ phòng ban",
                Content = "Bạn vừa tạo mới danh sách phòng ban hỗ trợ.",
                AltContent = $"Tạo mới {newSupporters.Count} phòng ban hỗ trợ.",
                Channel = NotificationChannel.Realtime
            });

            // 5️⃣ Trả kết quả
            return _mapper.Map<List<CreateDepartmentSupportResponseDto>>(newSupporters);
        }

        public PageResultDto<PageDepartmentSupportResultDto> PagingDepartmentSupport(FilterDepartmentSupportDto dto)
        {
            _logger.LogInformation($"{nameof(PagingDepartmentSupport)} method called, dto: {JsonSerializer.Serialize(dto)}.");

            var userId = CommonUntil.GetCurrentUserId(_contextAccessor);
            var user = _unitOfWork.iNsNhanSuRepository.TableNoTracking
                                    .FirstOrDefault(u => u.Id == userId);

            var department = _unitOfWork.iDepartmentSupportRepository.TableNoTracking.Include(x => x.Supporters).Include(x => x.DelegationIncoming);
            var keyword = dto.Keyword?.Trim().ToLower();

            var query =
                from ds in department
                join pb in _unitOfWork.iDmPhongBanRepository.TableNoTracking
                    on ds.DepartmentSupportId equals pb.Id
                where !ds.Deleted
                && (string.IsNullOrEmpty(keyword)
                    || (ds.DelegationIncoming != null
                        && ds.DelegationIncoming.Name.ToLower().Contains(keyword)))
                // Ban giám hiệu id = 5
                && (user.HienTaiPhongBan == pb.Id 
                ||  user.HienTaiPhongBan == 5 
                ||  user.UserType == UserTypeConstant.SUPER_ADMIN
                ||  user.UserType == UserTypeConstant.ADMIN)
                select new PageDepartmentSupportResultDto
                {
                    Id = ds.Id,
                    DepartmentSupportId = pb.Id,
                    DepartmentSupportName = pb.TenPhongBan,
                    DelegationIncomingId = ds.DelegationIncoming.Id,
                    DelegationIncomingName = ds.DelegationIncoming.Name,
                    Content = ds.Content,
                    Supporters = ds.Supporters.Where(s => !s.Deleted).ToList(),
                };

            var totalCount = query.Count();

            var items = query
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .ToList();

            return new PageResultDto<PageDepartmentSupportResultDto>
            {
                Items = items,
                TotalItem = totalCount
            };
        }
        public List<ViewDelegationIncomingResponseDto> GetAllDelegationIncoming(ViewDelegationIncomingRequestDto dto)
        {
            _logger.LogInformation($"{nameof(GetAllDelegationIncoming)} called.");

            var list = _unitOfWork.iDelegationIncomingRepository.TableNoTracking
                .Where(x => !x.Deleted)
                .Select(x => new ViewDelegationIncomingResponseDto
                {
                    IdDelegationIncoming = x.Id,
                    TenDoanVao = x.Name,
                    DelegationIncomingCode = x.Code,
                })
                .ToList();

            return list;
        }

        public async Task<UpdateDepartmentSupportResponseDto> UpdateDepartmentSupport(UpdateDepartmentSupportRequestDto dto)
        {
            _logger.LogInformation($"{nameof(UpdateDepartmentSupport)} called. Dto: {JsonSerializer.Serialize(dto)}");

            dto.Supporters ??= new();

            var departmentSupport = _unitOfWork
                .iDepartmentSupportRepository
                .Table
                .Include(x => x.Supporters)
                .FirstOrDefault(x => x.Id == dto.Id );

            if (departmentSupport == null)
                throw new Exception("Không tìm thấy Phòng ban hỗ trợ.");

            // Update Phòng ban hỗ trợ
            departmentSupport.Content = dto.Content;
            departmentSupport.DelegationIncomingId = dto.DelegationIncomingId;

            // Check trùng code trong request
            var duplicateCodes = dto.Supporters
                .Where(x => !string.IsNullOrWhiteSpace(x.SupporterCode))
                .GroupBy(x => x.SupporterCode)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicateCodes.Any())
                throw new Exception($"Mã người hỗ trợ bị trùng trong request: {string.Join(", ", duplicateCodes)}");

            // Check trùng code trong DB
            var allSupporters = departmentSupport.Supporters.ToList();

            foreach (var item in dto.Supporters)
            {
                if (string.IsNullOrWhiteSpace(item.SupporterCode))
                    continue;

                var existed = allSupporters.FirstOrDefault(x =>
                    x.SupporterCode == item.SupporterCode &&
                    (x.SupporterId != item.SupporterId));

                if (existed != null)
                {
                    throw new Exception($"Mã người hỗ trợ '{item.SupporterCode}' đã tồn tại.");
                }
            }
            // Xoá mềm những nguòi nếu request gửi lên ko có 
            var requestIds = dto.Supporters      
                .Select(x => x.SupporterId)
                .ToList();

            var supportersToSoftDelete = departmentSupport.Supporters
                .Where(x => !x.Deleted && !requestIds.Contains(x.SupporterId))
                .ToList();

            foreach (var supporter in supportersToSoftDelete)
            {
                supporter.Deleted = true;
            }
            // Update supporter
            foreach (var item in dto.Supporters)
            {
                // check nhân sự có tồn tại không
                var nhanSuExist = await _unitOfWork.iNsNhanSuRepository
                    .TableNoTracking
                    .AnyAsync(x => x.Id == item.SupporterId && !x.Deleted);

                if (!nhanSuExist)
                    throw new Exception($"Không tồn tại nhân sự ID = {item.SupporterId}");

                // tìm supporter trong bảng Supporter
                var supporter = await _unitOfWork.iSupporterRepository
                    .Table
                    .FirstOrDefaultAsync(x => x.SupporterId == item.SupporterId);

                if (supporter != null)
                {
                    // đã có supporterId → gán và cập nhật
                    supporter.SupporterCode = item.SupporterCode;
                    supporter.DepartmentSupportId = departmentSupport.Id;
                    supporter.Deleted = false;
                }
                else
                {
                    // Tạo mới supporter khi chưa có trong bảng supporter
                    var supporterNew = new Supporter
                    {
                        SupporterId = item.SupporterId,
                        SupporterCode = item.SupporterCode,
                        DepartmentSupportId = departmentSupport.Id,
                        Deleted = false
                    };

                    _unitOfWork.iSupporterRepository.Add(supporterNew);
                }
            }
            _unitOfWork.iDepartmentSupportRepository.Update(departmentSupport);
            await _unitOfWork.SaveChangesAsync();

            return new UpdateDepartmentSupportResponseDto
            {
                Id = departmentSupport.Id,
                DepartmentSupportId = departmentSupport.DepartmentSupportId,
                DelegationIncomingId = departmentSupport.DelegationIncomingId,
                Content = departmentSupport.Content,
                Supporters = departmentSupport.Supporters
                    .Where(x => !x.Deleted)
                    .Select(x => new UpdateSupporterItemDto
                    {
                        SupporterId = x.SupporterId,
                        SupporterCode = x.SupporterCode
                    })
                    .ToList()
            };
        }

        public async Task<DetailDepartmentSupportResponseDto?> GetByIdDepartmentSupport(int id)
        {
            _logger.LogInformation($"{nameof(GetByIdDepartmentSupport)} called with id: {id}");

            var detail = await _unitOfWork.iDepartmentSupportRepository
                .TableNoTracking
                .Include(x => x.DelegationIncoming)
                .Include(x => x.Supporters)
                .FirstOrDefaultAsync(x => x.Id == id && !x.Deleted);

            if (detail == null)
                return null;

            var phongBan = await _unitOfWork.iDmPhongBanRepository
                .TableNoTracking
                .FirstOrDefaultAsync(x => x.Id == detail.DepartmentSupportId);

            var result = new DetailDepartmentSupportResponseDto
            {
                Id = detail.Id,
                DepartmentSupportId = detail.DepartmentSupportId,
                DelegationIncomingId = detail.DelegationIncomingId,

                DepartmentSupportName = phongBan?.TenPhongBan,
                DelegationIncomingName = detail.DelegationIncoming?.Name,

                Content = detail.Content,

                Supporters = detail.Supporters
                    .Where(s => !s.Deleted)
                    .Select(s => new DepartmentSupporterDto
                    {
                        SupporterId = s.SupporterId,
                        SupporterCode = s.SupporterCode
                    })            
                    .ToList()
            };

            return result;
        }
        public void DeleteDepartmentSupport(int id)
        {
            _logger.LogInformation($"{nameof(DeleteDepartmentSupport)} method called. Dto: {id}");

            var exist = _unitOfWork.iDepartmentSupportRepository.FindById(id);

            if (exist != null)
            {
                _unitOfWork.iDepartmentSupportRepository.Delete(exist);
                _unitOfWork.iDepartmentSupportRepository.SaveChange();
            }
            else
            {
                throw new Exception($"Phòng ban hỗ trợ không tồn tại hoặc đã bị xóa");
            }
        }



    }
}
