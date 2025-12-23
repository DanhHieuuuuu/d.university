using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming
{
    public class DetailDepartmentSupportResponseDto
    {
        public int DepartmentSupportId { get; set; }

        public int DelegationIncomingId { get; set; }

        public string? DepartmentSupportName { get; set; } 

        public string? DelegationIncomingName { get; set; } 

        public string? Content { get; set; } 

        public List<DepartmentSupporterDto> Supporters { get; set; } = new();
    }

    public class DepartmentSupporterDto
    {
        public int SupporterId { get; set; }

        public string? SupporterCode { get; set; }
    }
}
