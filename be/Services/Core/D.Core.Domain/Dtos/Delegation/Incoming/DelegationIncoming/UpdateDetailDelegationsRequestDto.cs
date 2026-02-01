using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming
{
    public class UpdateDetailDelegationsRequestDto : ICommand<List<UpdateDetailDelegationResponseDto>>
    {
        public List<UpdateDetailDelegationItemDto> Items { get; set; } = new();
    }
    public class UpdateDetailDelegationItemDto
    {
        public int? Id { get; set; }
        public int DelegationIncomingId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int YearOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public bool IsLeader { get; set; }
    }
}


