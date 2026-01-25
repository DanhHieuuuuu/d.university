using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming
{
    public class CreateDepartmentSupportRequestDto : ICommand <List<CreateDepartmentSupportResponseDto>>
    {
        public int DelegationIncomingId { get; set; }
        public List<int> DepartmentSupportIds { get; set; }
        public string? Content { get; set; }
    }
}
