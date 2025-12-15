using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming
{
    public class UpdateReceptionTimeResponseDto 
    {
        public int Id { get; set; }

        public TimeOnly StartDate { get; set; }

        public TimeOnly EndDate { get; set; }

        public DateOnly Date { get; set; }

        public string Content { get; set; } = string.Empty;

        public int TotalPerson { get; set; }

        public string Address { get; set; } = string.Empty;
    }
}
