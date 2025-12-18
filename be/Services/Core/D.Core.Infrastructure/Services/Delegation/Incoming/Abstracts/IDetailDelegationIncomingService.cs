using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts
{
    public interface IDetailDelegationIncomingService
    {
        Task<DetailDelegationIncomingResponseDto> GetByIdDetailDelegation(int delegationIncomingId);
    }
}
