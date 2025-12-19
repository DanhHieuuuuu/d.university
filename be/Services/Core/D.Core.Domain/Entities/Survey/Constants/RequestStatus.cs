using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace D.Core.Domain.Entities.Survey.Constants
{
    public static class RequestStatus
    {
        public const int Draft = 1;
        public const int Pending = 2;
        public const int Approved = 3;
        public const int Rejected = 4;
        public const int Canceled = 5;
        public const int NeedEdit = 6;

        public static Dictionary<int, string> Names = new Dictionary<int, string>()
        {
            { Draft, "Bản nháp" },
            { Pending, "Đề xuất" },
            { Approved, "Phê duyệt" },
            { Rejected, "Từ chối" },
            { Canceled, "Bị hủy/đóng" },
            { NeedEdit, "Cần bổ sung" },
        };
    }
}
