using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Entities.Survey.Constants
{
    public static class SubmissionStatus
    {
        public const int InProgress = 1;
        public const int Submitted = 2;

        public static Dictionary<int, string> Names = new Dictionary<int, string>()
        {
            { InProgress, "Đang làm" },
            { Submitted, "Đã nộp" },
        };
    }
}
