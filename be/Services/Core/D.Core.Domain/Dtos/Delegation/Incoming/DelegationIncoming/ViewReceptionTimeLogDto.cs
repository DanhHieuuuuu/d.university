using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming
{
    public class ViewReceptionTimeLogDto
    {
        public int Id { get; set; }

        public int ReceptionTimeId { get; set; }

        public string Type { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? Reason { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }
    }
}
