using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.AI
{
    public class DifyDataDto
    {
        public string Id { get; set; }
        public string WorkflowId { get; set; }
        public string Status { get; set; }
        public DifyOutputsDto Outputs { get; set; }
    }
}
