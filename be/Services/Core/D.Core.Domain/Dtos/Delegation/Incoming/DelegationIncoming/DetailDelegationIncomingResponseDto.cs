using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming
{
    public class DetailDelegationIncomingResponseDto
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int YearOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public bool IsLeader { get; set; }
        public int DelegationIncomingId { get; set; }
        public string? DelegationName { get; set; } 
        public string? DelegationCode { get; set; } 
    }
}
