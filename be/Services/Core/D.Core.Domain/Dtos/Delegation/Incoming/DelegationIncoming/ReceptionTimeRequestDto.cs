using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming
{
    public class ReceptionTimeRequestDto : IQuery<List<ReceptionTimeResponseDto>>
    {
        public int DelegationIncomingId {  get; set; }
    }
}
