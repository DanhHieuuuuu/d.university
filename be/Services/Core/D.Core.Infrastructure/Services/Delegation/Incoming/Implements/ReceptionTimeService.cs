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
        public async Task<CreateReceptionTimeResponseDto> CreateReceptionTime(CreateReceptionTimeRequestDto dto)
        {
            _logger.LogInformation($"{nameof(CreateReceptionTime)} method called, dto: {JsonSerializer.Serialize(dto)}.");

            var newReceptionTime = _mapper.Map<ReceptionTime>(dto);          
            _unitOfWork.iReceptionTimeRepository.Add(newReceptionTime);
            await _unitOfWork.SaveChangesAsync();
            #region Log         
            var userId = CommonUntil.GetCurrentUserId(_contextAccessor);
            var user = _unitOfWork.iNsNhanSuRepository.TableNoTracking
            .FirstOrDefault(u => u.Id == userId);
            var userName = user != null ? $"{user.HoDem} {user.Ten}" : "Unknown";

            var log = new LogReceptionTime
            {
                ReceptionTimeId = newReceptionTime.Id, 
                Type = LogType.Create,
                Description = $"Thêm thời gian tiếp đoàn bởi {userName} lúc {DateTime.Now:dd/MM/yyyy HH:mm:ss}",
                Reason = DelegationStatus.Names[DelegationStatus.Create],
                CreatedDate = DateTime.Now,
                CreatedBy = userId.ToString()
            };

            _unitOfWork.iLogReceptionTimeRepository.Add(log);
            await _unitOfWork.SaveChangesAsync();
            #endregion
            return _mapper.Map<CreateReceptionTimeResponseDto>(newReceptionTime);

        }       
        public async Task<ReceptionTimeResponseDto> GetByIdReceptionTime(int delegationIncomingId)
        {
            _logger.LogInformation($"{nameof(GetByIdReceptionTime)} called with DelegationIncomingId: {delegationIncomingId}");

            // Lấy receptionTime theo DelegationIncomingId
            var receptionTime = _unitOfWork.iReceptionTimeRepository.TableNoTracking
                .FirstOrDefault(r => r.DelegationIncomingId == delegationIncomingId);

            if (receptionTime == null)
                return null;

            // Lấy thông tin đoàn
            var delegation = _unitOfWork.iDelegationIncomingRepository.TableNoTracking
                .FirstOrDefault(d => d.Id == receptionTime.DelegationIncomingId);

            var result = new ReceptionTimeResponseDto
            {
                Id = receptionTime.Id,
                StartDate = receptionTime.StartDate,
                EndDate = receptionTime.EndDate,
                Date = receptionTime.Date,
                Content = receptionTime.Content,
                TotalPerson = receptionTime.TotalPerson,
                Address = receptionTime.Address,
                DelegationIncomingId = receptionTime.DelegationIncomingId,
                DelegationName = delegation?.Name,
                DelegationCode = delegation?.Code
            };

            return result;
        }

        public async Task<UpdateReceptionTimeResponseDto> UpdateReceptionTime(UpdateReceptionTimeRequestDto dto)
        {
            _logger.LogInformation(
                $"{nameof(UpdateReceptionTime)} called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var exist = _unitOfWork.iReceptionTimeRepository.Table
                .FirstOrDefault(x => x.DelegationIncomingId == dto.DelegationIncomingId);

            if (exist == null)
                throw new Exception("Không tìm thấy thời gian tiếp đoàn.");

            #region Save old values
            var oldStartDate = exist.StartDate;
            var oldEndDate = exist.EndDate;
            var oldDate = exist.Date;
            var oldContent = exist.Content;
            var oldTotalPerson = exist.TotalPerson;
            var oldAddress = exist.Address;
            #endregion

            #region Update entity
            exist.StartDate = dto.StartDate;
            exist.EndDate = dto.EndDate;
            exist.Date = dto.Date;
            exist.Content = dto.Content;
            exist.TotalPerson = dto.TotalPerson;
            exist.Address = dto.Address;
            #endregion

            _unitOfWork.iReceptionTimeRepository.Update(exist);
            await _unitOfWork.SaveChangesAsync();

            #region Log
            var userId = CommonUntil.GetCurrentUserId(_contextAccessor);
            var user = _unitOfWork.iNsNhanSuRepository.TableNoTracking
                .FirstOrDefault(u => u.Id == userId);
            var userName = user != null ? $"{user.HoDem} {user.Ten}" : "Unknown";

            var changes = new List<string>();

            if (oldStartDate != dto.StartDate)
                changes.Add($"Giờ bắt đầu: {oldStartDate} → {dto.StartDate}");

            if (oldEndDate != dto.EndDate)
                changes.Add($"Giờ kết thúc: {oldEndDate} → {dto.EndDate}");

            if (oldDate != dto.Date)
                changes.Add($"Ngày: {oldDate} → {dto.Date}");

            if (oldContent != dto.Content)
                changes.Add($"Nội dung: '{oldContent}' → '{dto.Content}'");

            if (oldTotalPerson != dto.TotalPerson)
                changes.Add($"Số người: {oldTotalPerson} → {dto.TotalPerson}");

            if (oldAddress != dto.Address)
                changes.Add($"Địa điểm: '{oldAddress}' → '{dto.Address}'");

            var description = changes.Any()
                ? $"Cập nhật thời gian tiếp đoàn: {string.Join("; ", changes)}. Bởi {userName} lúc {DateTime.Now:dd/MM/yyyy HH:mm:ss}"
                : $"Cập nhật thời gian tiếp đoàn nhưng không thay đổi dữ liệu. Bởi {userName} lúc {DateTime.Now:dd/MM/yyyy HH:mm:ss}";

            var log = new LogReceptionTime
            {
                ReceptionTimeId = exist.Id,
                Type = LogType.Update,
                Description = description,
                Reason = DelegationStatus.Names[DelegationStatus.Edited],
                CreatedDate = DateTime.Now,
                CreatedBy = userId.ToString()
            };

            _unitOfWork.iLogReceptionTimeRepository.Add(log);
            await _unitOfWork.SaveChangesAsync();
            #endregion

            return _mapper.Map<UpdateReceptionTimeResponseDto>(exist);
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
