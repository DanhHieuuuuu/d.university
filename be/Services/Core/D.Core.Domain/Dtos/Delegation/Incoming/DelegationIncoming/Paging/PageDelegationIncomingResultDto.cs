using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming.Paging
{
    /// <summary>
    /// Kết quả trả về của paging
    /// </summary>
    public class PageDelegationIncomingResultDto
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public int IdPhongBan { get; set; }

        public string? Location { get; set; }

        public int IdStaffReception { get; set; }

        public int TotalPerson { get; set; }

        public string? PhoneNumber { get; set; }

        public int Status { get; set; }

        public DateOnly RequestDate { get; set; }

        public DateOnly ReceptionDate { get; set; }

        public decimal TotalMoney { get; set; }
    }
}
