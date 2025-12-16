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
        public async Task<DetailDelegationIncomingResponseDto> GetByIdDetailDelegation(int id)
        {
            _logger.LogInformation($"{nameof(GetByIdDetailDelegation)} called with id: {id}");

            var detail = _unitOfWork.iDetailDelegationIncomingRepository.TableNoTracking
                .FirstOrDefault(d => d.Id == id);

            if (detail == null)
            {
                return null;
            }

            var delegation = _unitOfWork.iDelegationIncomingRepository.TableNoTracking
                .FirstOrDefault(d => d.Id == detail.DelegationIncomingId);

            var result = new DetailDelegationIncomingResponseDto
            {
                Id = detail.Id,
                Code = detail.Code,
                FirstName = detail.FirstName,
                LastName = detail.LastName,
                YearOfBirth = detail.YearOfBirth,
                PhoneNumber = detail.PhoneNumber,
                Email = detail.Email,
                IsLeader = detail.IsLeader,
                DelegationIncomingId = detail.DelegationIncomingId,
                DelegationName = delegation != null ? delegation.Name : null,
                DelegationCode = delegation != null ? delegation.Code : null
            };

            return result;
        }
    }
}
