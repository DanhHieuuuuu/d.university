using AutoMapper;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming.Paging;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using D.Core.Domain.Entities.Delegation.Incoming;
using D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using D.Notification.Domain.Enums;
using D.Notification.Dtos;
using D.Notification.ApplicationService.Abstracts;
using D.InfrastructureBase.Shared;

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

        public async Task<CreateDepartmentSupportResponseDto> CreateDepartmentSupport(CreateDepartmentSupportRequestDto dto)
        {
            _logger.LogInformation($"{nameof(CreateDepartmentSupport)} method called, dto: {JsonSerializer.Serialize(dto)}.");

            //Kiểm tra departmentSupportId có tồn tại trong phòng ban 
            var phongBanExist = await _unitOfWork.iDmPhongBanRepository
                .TableNoTracking
                .AnyAsync(x => x.Id == dto.DepartmentSupportId && !x.Deleted);
            var userId = CommonUntil.GetCurrentUserId(_contextAccessor);
            if (!phongBanExist)
                throw new Exception("Không tồn tại phòng ban này");
            //Tạo DepartmentSupport
            var newSupporter = _mapper.Map<DepartmentSupport>(dto);
            _unitOfWork.iDepartmentSupportRepository.Add(newSupporter);
            await _unitOfWork.SaveChangesAsync();
            await _notificationService.SendAsync(new NotificationMessage
            {
                Receiver = new Receiver
                {
                    // Người nhận thông báo
                    UserId = userId,
                },
                Title = "Phân công hỗ trợ phòng ban",
                Content = "Bạn vừa tạo mới hỗ trợ phòng ban.",
                AltContent = $"Tạo mới phòng ban hỗ trợ (ID: {dto.DepartmentSupportId}).",
                Channel = NotificationChannel.Realtime
            });
            return _mapper.Map<CreateDepartmentSupportResponseDto>(newSupporter);

        }
        public PageResultDto<PageDepartmentSupportResultDto> PagingDepartmentSupport(FilterDepartmentSupportDto dto)
        {
            _logger.LogInformation($"{nameof(PagingDepartmentSupport)} method called, dto: {JsonSerializer.Serialize(dto)}.");

            var department = _unitOfWork.iDepartmentSupportRepository.TableNoTracking.Include(x => x.Supporters).Include(x => x.DelegationIncoming);

            var query =
                from ds in department
                join pb in _unitOfWork.iDmPhongBanRepository.TableNoTracking
                    on ds.DepartmentSupportId equals pb.Id
                where !ds.Deleted
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
                .FirstOrDefault(x => x.DepartmentSupportId == dto.DepartmentSupportId);

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
                throw new Exception($"SupporterCode bị trùng trong request: {string.Join(", ", duplicateCodes)}");

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
                    throw new Exception($"SupporterCode '{item.SupporterCode}' đã tồn tại.");
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
                if (item.SupporterId > 0)
                {
                    var supporter = departmentSupport.Supporters
                        .FirstOrDefault(x => x.SupporterId == item.SupporterId);

                    if (supporter != null)
                    {
                        supporter.SupporterCode = item.SupporterCode;
                        supporter.Deleted = false;
                    }
                    else
                    {
                        var supporterNew = new Supporter
                        {
                            SupporterId = item.SupporterId,
                            SupporterCode = item.SupporterCode,
                            DepartmentSupportId = departmentSupport.DepartmentSupportId,
                            Deleted = false
                        };
                        departmentSupport.Supporters.Add(supporterNew);
                    }
                }
         
            }

            await _unitOfWork.SaveChangesAsync();

            return new UpdateDepartmentSupportResponseDto
            {
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
                DepartmentSupportId = detail.Id,
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




    }
}
