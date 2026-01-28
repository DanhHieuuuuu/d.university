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



    }
}
