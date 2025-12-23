using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming
{
    public class CreateDepartmentSupportResponseDto
    {
        public int Id { get; set; }
        public int DepartmentSupportId { get; set; }

        public int DelegationIncomingId { get; set; }

        public string? Content { get; set; }

        public int TotalSupporter { get; set; }
    }
}
