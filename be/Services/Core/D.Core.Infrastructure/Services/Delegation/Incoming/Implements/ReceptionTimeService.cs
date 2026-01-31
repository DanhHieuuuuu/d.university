using AutoMapper; 
using D.Constants.Core.Delegation; 
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming; 
using D.Core.Domain.Entities.Delegation.Incoming; 
using D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts; 
using D.InfrastructureBase.Service; 
using D.InfrastructureBase.Shared; 
using Microsoft.AspNetCore.Http; 
using Microsoft.Extensions.Logging; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Text.Json;  
using System.Threading.Tasks; 
 
namespace D.Core.Infrastructure.Services.Delegation.Incoming.Implements 
{  
    public class ReceptionTimeService : ServiceBase, IReceptionTimeService 
    {  
        private readonly ServiceUnitOfWork _unitOfWork; 
 
        public ReceptionTimeService( 
            ILogger<ReceptionTimeService> logger, 
            IHttpContextAccessor httpContext, 
            IMapper mapper, 
            ServiceUnitOfWork unitOfWork 
        ) 
            : base(logger, httpContext, mapper) 
        { 
            _unitOfWork = unitOfWork; 
        } 
        public async Task<List<CreateReceptionTimeResponseDto>> CreateReceptionTimeList(CreateReceptionTimeListRequestDto dto) 
        { 
            _logger.LogInformation($"{nameof(CreateReceptionTimeList)} called, dto: {JsonSerializer.Serialize(dto)}"); 
 
            var receptionTimes = _mapper.Map<List<ReceptionTime>>(dto.Items);
            var deleagtion = _unitOfWork.iDelegationIncomingRepository.FindById(dto.Items[0].DelegationIncomingId);
            _unitOfWork.iReceptionTimeRepository.AddRange(receptionTimes); 
            #region Log 
            var userId = CommonUntil.GetCurrentUserId(_contextAccessor); 
            var user = _unitOfWork.iNsNhanSuRepository.TableNoTracking 
                .FirstOrDefault(u => u.Id == userId); 
 
            var userName = user != null ? $"{user.HoDem} {user.Ten}" : "Unknown"; 
 
            var logs = receptionTimes.Select(rt => new LogReceptionTime 
            { 
                ReceptionTimeId = rt.Id, 
                Type = LogType.Create, 
                Description = $"Thêm thời gian tiếp đoàn {deleagtion.Name} ({deleagtion.Code})", 
                Reason = DelegationStatus.Names[DelegationStatus.Create], 
                CreatedDate = DateTime.Now, 
                CreatedBy = userId.ToString(), 
                CreatedByName = userName 
            }).ToList(); 
 
            _unitOfWork.iLogReceptionTimeRepository.AddRange(logs); 
            #endregion 
            await _unitOfWork.SaveChangesAsync(); 
 
            return _mapper.Map<List<CreateReceptionTimeResponseDto>>(receptionTimes); 
        } 
 
        public async Task<List<ReceptionTimeResponseDto>> GetByIdReceptionTime(int delegationIncomingId) 
        { 
            _logger.LogInformation( 
                $"{nameof(GetByIdReceptionTime)} called with DelegationIncomingId: {delegationIncomingId}" 
            ); 
 
            var receptionTimes = _unitOfWork.iReceptionTimeRepository.TableNoTracking 
                .Where(r => r.DelegationIncomingId == delegationIncomingId) 
                .OrderBy(r => r.Date) 
                .ThenBy(r => r.StartDate) 
                .ToList(); 
 
            if (!receptionTimes.Any()) 
                return new List<ReceptionTimeResponseDto>(); 
 
            var delegation = _unitOfWork.iDelegationIncomingRepository.TableNoTracking 
                .FirstOrDefault(d => d.Id == delegationIncomingId); 
            //Load Prepare theo các ReceptionTimeId 
            var receptionTimeIds = receptionTimes.Select(x => x.Id).ToList(); 
 
            var prepares = _unitOfWork.iPrepareRepository.TableNoTracking 
                .Where(p => receptionTimeIds.Contains(p.ReceptionTimeId) && !p.Deleted) 
                .ToList(); 
 
            var result = receptionTimes.Select(rt => new ReceptionTimeResponseDto 
            { 
                Id = rt.Id, 
                StartDate = rt.StartDate, 
                EndDate = rt.EndDate, 
                Date = rt.Date, 
                Content = rt.Content, 
                TotalPerson = rt.TotalPerson, 
                Address = rt.Address, 
                DelegationIncomingId = rt.DelegationIncomingId, 
                DelegationName = delegation?.Name, 
                DelegationCode = delegation?.Code, 
                Prepares = prepares 
                     .Where(p => p.ReceptionTimeId == rt.Id) 
                     .Select(p => new PrepareResponseDto 
                     { 
                         Id = p.Id, 
                         Name = p.Name, 
                         Description = p.Description, 
                         Money = p.Money 
                     }) 
                     .ToList() 
            }).ToList(); 
 
            return result; 
        } 
 
        public async Task<List<UpdateReceptionTimeResponseDto>> UpdateReceptionTimes(List<UpdateReceptionTimeRequestDto> dtos) 
        { 
            if (dtos == null || !dtos.Any())throw new Exception("Danh sách thời gian tiếp đoàn trống."); 
 
            var delegationId = dtos.First().DelegationIncomingId; 
 
            var dbItems = _unitOfWork.iReceptionTimeRepository.TableNoTracking 
                .Where(x => x.DelegationIncomingId == delegationId && !x.Deleted) 
                .ToList(); 
 
            var userId = CommonUntil.GetCurrentUserId(_contextAccessor); 
            var user = _unitOfWork.iNsNhanSuRepository.TableNoTracking 
                .FirstOrDefault(u => u.Id == userId); 
            var userName = user != null ? $"{user.HoDem} {user.Ten}" : "Unknown"; 
 
            var logs = new List<LogReceptionTime>(); 
 
            // UPDATE 
            foreach (var dto in dtos.Where(x => x.Id.HasValue)) 
            { 
                var exist = dbItems.FirstOrDefault(x => x.Id == dto.Id.Value); 
                if (exist == null) continue; 
 
                var changes = new List<string>(); 
 
                if (exist.StartDate != dto.StartDate) 
                    changes.Add($"Giờ bắt đầu: {exist.StartDate} → {dto.StartDate}"); 
 
                if (exist.EndDate != dto.EndDate) 
                    changes.Add($"Giờ kết thúc: {exist.EndDate} → {dto.EndDate}"); 
 
                if (exist.Date != dto.Date) 
                    changes.Add($"Ngày: {exist.Date} → {dto.Date}"); 
 
                if (exist.Content != dto.Content) 
                    changes.Add($"Nội dung: '{exist.Content}' → '{dto.Content}'"); 
 
                if (exist.TotalPerson != dto.TotalPerson) 
                    changes.Add($"Số người: {exist.TotalPerson} → {dto.TotalPerson}"); 
 
                if (exist.Address != dto.Address) 
                    changes.Add($"Địa điểm: '{exist.Address}' → '{dto.Address}'"); 
 
                exist.StartDate = dto.StartDate; 
                exist.EndDate = dto.EndDate; 
                exist.Date = dto.Date; 
                exist.Content = dto.Content; 
                exist.TotalPerson = dto.TotalPerson; 
                exist.Address = dto.Address; 
 
                _unitOfWork.iReceptionTimeRepository.Update(exist); 
 
                logs.Add(new LogReceptionTime 
                { 
                    ReceptionTimeId = exist.Id, 
                    Type = LogType.Update, 
                    Description = changes.Any() 
                        ? $"Cập nhật thời gian tiếp đoàn: {string.Join("; ", changes)}." 
                        : "Cập nhật thời gian tiếp đoàn nhưng không thay đổi dữ liệu.", 
                    Reason = DelegationStatus.Names[DelegationStatus.Edited], 
                    CreatedDate = DateTime.Now, 
                    CreatedBy = userId.ToString(), 
                    CreatedByName = userName 
                }); 
            } 
 
            // SOFT DELETE 
            var clientIds = dtos 
                .Where(x => x.Id.HasValue) 
                .Select(x => x.Id.Value) 
                .ToList(); 
 
            var softDeleteItems = dbItems 
                .Where(x => !clientIds.Contains(x.Id)) 
                .ToList(); 
 
            foreach (var item in softDeleteItems) 
            { 
                item.Deleted = true; 
                _unitOfWork.iReceptionTimeRepository.Update(item); 
 
                logs.Add(new LogReceptionTime 
                { 
                    ReceptionTimeId = item.Id, 
                    Type = LogType.Delete, 
                    Description = $"Xoá thời gian tiếp đoàn.", 
                    Reason = DelegationStatus.Names[DelegationStatus.Edited], 
                    CreatedDate = DateTime.Now, 
                    CreatedBy = userId.ToString(), 
                    CreatedByName = userName 
                }); 
            } 
 
            await _unitOfWork.SaveChangesAsync(); 
 
            if (logs.Any()) 
            { 
                _unitOfWork.iLogReceptionTimeRepository.AddRange(logs); 
                await _unitOfWork.SaveChangesAsync(); 
            } 
 
            var result = _unitOfWork.iReceptionTimeRepository.Table 
                .Where(x => x.DelegationIncomingId == delegationId && !x.Deleted) 
                .ToList(); 
 
            return _mapper.Map<List<UpdateReceptionTimeResponseDto>>(result); 
        } 
 
 
        public void DeleteReceptionTime(int id) 
        {  
            _logger.LogInformation($"{nameof(DeleteReceptionTime)} method called. Dto: {id}"); 
 
            var exist = _unitOfWork.iReceptionTimeRepository.FindById(id); 
 
            if (exist != null) 
            { 
                _unitOfWork.iReceptionTimeRepository.Delete(exist); 
                _unitOfWork.iReceptionTimeRepository.SaveChange(); 
            } 
            else 
            { 
                throw new Exception($"Thời gian tiếp đoàn không tồn tại hoặc đã bị xóa"); 
            } 
        } 
 
    } 
} 
