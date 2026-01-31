using AutoMapper;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts;
using D.InfrastructureBase.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming.DetailDelegationIncomingResponseDto;

namespace D.Core.Infrastructure.Services.Delegation.Incoming.Implements
{
    public class DetailDelegationIncomingService : ServiceBase, IDetailDelegationIncomingService
    {
        private readonly ServiceUnitOfWork _unitOfWork;

        public DetailDelegationIncomingService(
            ILogger<DetailDelegationIncomingService> logger,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork
        )
            : base(logger, httpContext, mapper)
        {
            _unitOfWork = unitOfWork;
        }
       public async Task<DetailDelegationIncomingResponseDto> GetByIdDetailDelegation(int delegationIncomingId)
        {
            _logger.LogInformation($"{nameof(GetByIdDetailDelegation)} called with DelegationIncomingId: {delegationIncomingId}");
        
            var delegation = _unitOfWork.iDelegationIncomingRepository.TableNoTracking
                .FirstOrDefault(d => d.Id == delegationIncomingId);
        
            if (delegation == null)
                return null;
        
            // Danh sách người tiếp đoàn
            var members = _unitOfWork.iDetailDelegationIncomingRepository.TableNoTracking
                .Where(d => d.DelegationIncomingId == delegationIncomingId)
                .Select(d => new MemberDto
                {
                    Id = d.Id,
                    Code = d.Code,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    YearOfBirth = d.YearOfBirth,
                    PhoneNumber = d.PhoneNumber,
                    Email = d.Email,
                    IsLeader = d.IsLeader
                }).ToList();
        
            // Phòng ban, người hỗ trợ
            var phongBanTable = _unitOfWork.iDmPhongBanRepository.TableNoTracking;
            var staffTable = _unitOfWork.iNsNhanSuRepository.TableNoTracking;
        
            var departmentSupports =
                (from ds in _unitOfWork.iDepartmentSupportRepository.TableNoTracking
                 join pb in phongBanTable on ds.DepartmentSupportId equals pb.Id
                 where !ds.Deleted
                       && ds.DelegationIncomingId == delegationIncomingId
                 select new DepartmentSupportDto
                 {
                     DepartmentSupportId = pb.Id,
                     DepartmentSupportName = pb.TenPhongBan,
                     Supporters =
                        (from sp in _unitOfWork.iSupporterRepository.TableNoTracking
                         join ns in staffTable on sp.SupporterId equals ns.Id
                         where !sp.Deleted
                               && sp.DepartmentSupportId == ds.DepartmentSupportId
                         select new SupporterDto
                         {
                             Id = sp.Id,
                             SupporterCode = sp.SupporterCode,
                             SupporterName = ns.HoDem + " " + ns.Ten
                         }).ToList()
                 }).ToList();
        
            return new DetailDelegationIncomingResponseDto
            {
                DelegationIncomingId = delegation.Id,
                DelegationCode = delegation.Code,
                DelegationName = delegation.Name,
                Members = members,
                DepartmentSupports = departmentSupports
            };
        }
        public async Task<List<UpdateDetailDelegationResponseDto>> UpdateDetailDelegationIncoming(List<UpdateDetailDelegationItemDto> dtos)
        {
            if (dtos == null || !dtos.Any())
                throw new Exception("Danh sách thành viên đoàn trống.");

            var delegationId = dtos.First().DelegationIncomingId;

            var dbItems = _unitOfWork.iDetailDelegationIncomingRepository.Table
                .Where(x => x.DelegationIncomingId == delegationId && !x.Deleted)
                .ToList();

            // ================= UPDATE =================
            foreach (var dto in dtos.Where(x => x.Id.HasValue))
            {           
                var isExist = _unitOfWork.iDetailDelegationIncomingRepository
                    .IsCodeExist(dto.Code, dto.DelegationIncomingId, dto.Id);

                if (isExist)
                    throw new Exception($"Mã {dto.Code} đã tồn tại trong đoàn.");

                var exist = dbItems.FirstOrDefault(x => x.Id == dto.Id.Value);
                if (exist == null) continue;

                exist.Code = dto.Code;
                exist.FirstName = dto.FirstName;
                exist.LastName = dto.LastName;
                exist.YearOfBirth = dto.YearOfBirth;
                exist.PhoneNumber = dto.PhoneNumber;
                exist.Email = dto.Email;
                exist.IsLeader = dto.IsLeader;

                _unitOfWork.iDetailDelegationIncomingRepository.Update(exist);
            }

            // ================= SOFT DELETE =================
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
                _unitOfWork.iDetailDelegationIncomingRepository.Update(item);
            }

            await _unitOfWork.SaveChangesAsync();

            var result = _unitOfWork.iDetailDelegationIncomingRepository.Table
                .Where(x => x.DelegationIncomingId == delegationId && !x.Deleted)
                .ToList();

            return _mapper.Map<List<UpdateDetailDelegationResponseDto>>(result);
        }



    }
}
