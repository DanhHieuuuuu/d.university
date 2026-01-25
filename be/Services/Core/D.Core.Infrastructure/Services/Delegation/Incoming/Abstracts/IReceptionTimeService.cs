using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts
{
    public interface  IReceptionTimeService
    { 
        Task<List<ReceptionTimeResponseDto>> GetByIdReceptionTime(int delegationIncomingId); 
        Task<List<CreateReceptionTimeResponseDto>> CreateReceptionTimeList(CreateReceptionTimeListRequestDto dto); 
        void DeleteReceptionTime(int id); 
        Task<List<UpdateReceptionTimeResponseDto>> UpdateReceptionTimes(List<UpdateReceptionTimeRequestDto> dtos); 
    }
}
