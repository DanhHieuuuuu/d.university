using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Delegation.Incoming
{
    public class DelegationIncomingReportDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? PhongBan { get; set; }
        public string? StaffReception { get; set; }
        public int TotalPerson { get; set; }
        public int Status { get; set; }
        public DateOnly? RequestDate { get; set; }
        public DateOnly? ReceptionDate { get; set; }
        public decimal? TotalMoney { get; set; }
    }


}
