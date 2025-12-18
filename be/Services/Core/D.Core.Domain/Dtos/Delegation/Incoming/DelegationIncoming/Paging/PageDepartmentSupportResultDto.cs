using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming.Paging
{
    public class PageDepartmentSupportResultDto
    {
        public int Id { get; set; }

        public int DepartmentSupportId { get; set; }
        public string? DepartmentSupportName { get; set; }
        public int DelegationIncomingId { get; set; }
        public string? DelegationIncomingName { get; set; }
        public string? Content { get; set; }

    }
}
