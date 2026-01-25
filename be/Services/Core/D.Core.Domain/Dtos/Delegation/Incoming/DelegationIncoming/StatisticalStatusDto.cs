using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming
{
    public class StatisticalStatusDto
    {
        public int Status { get; set; }
        public int Total { get; set; }
    }
    public class StatisticalResultDto
    {
        public int TotalAll { get; set; }
        public List<StatisticalStatusDto>? ByStatus { get; set; }
    }
}
