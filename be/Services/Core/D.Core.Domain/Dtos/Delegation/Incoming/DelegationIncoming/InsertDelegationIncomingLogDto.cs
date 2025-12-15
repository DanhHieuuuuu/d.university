using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming
{
    public class InsertDelegationIncomingLogDto : ICommand
    {
        public int Id { get; set; }
        public string? DelegationIncomingCode { get; set; }
        public int? OldStatus { get; set; }
        public int? NewStatus { get; set; }
        public string? Description { get; set; }
        public string? Reason { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
