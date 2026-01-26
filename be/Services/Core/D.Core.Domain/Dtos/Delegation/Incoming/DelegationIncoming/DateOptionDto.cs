using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming
{
    public class DateOptionDto
    {
        public string Label { get; set; } = default!;
        public DateTime Value { get; set; }
    }
}
