using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Entities.Survey.Constants
{
    public static class SurveyStatus
    {
        public const int Close = 1;
        public const int Open = 2;
        public const int Completed = 3;

        public static Dictionary<int, string> Names = new Dictionary<int, string>()
        {
            { Close, "Đóng" },
            { Open, "Mở" },
            { Completed, "Hoàn thành" },
        };
    }
}