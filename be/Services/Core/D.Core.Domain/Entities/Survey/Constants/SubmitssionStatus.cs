using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Entities.Survey.Constants
{
    public static class SubmitssionStatus
    {
        public const int NotStarted = 1;
        public const int InProgress = 2;
        public const int Submitted = 3;

        public static Dictionary<int, string> Names = new Dictionary<int, string>()
        {
            { NotStarted, "Chưa làm" },
            { InProgress, "Đang làm" },
            { Submitted, "Đã nộp" },
        };
    }
}
