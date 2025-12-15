using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming
{
    public class UpdateDelegationIncomingResponseDto
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Content { get; set; }
        public int IdPhongBan { get; set; }
        public string? Location { get; set; }
        public int IdStaffReception { get; set; }
        public int TotalPerson { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly RequestDate { get; set; }
        public DateOnly ReceptionDate { get; set; }
        public decimal TotalMoney { get; set; }
        public int Status { get; set; }
    }
}
