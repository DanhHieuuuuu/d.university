using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.AI
{
    public class DifyWorkflowResponseDto
    {
        public string WorkflowRunId { get; set; }
        public string TaskId { get; set; }
        public DifyDataDto Data { get; set; }
    }
}
