using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming
{
    public class ViewDelegationIncomingResponseDto
    {
        public int IdDelegationIncoming { get; set; }
        public string? TenDoanVao { get; set; }
        public string? DelegationIncomingCode { get; set; }
    }
}
