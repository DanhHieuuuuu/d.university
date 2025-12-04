namespace D.Core.Domain.Entities.Kpi.Constants
{
    public class KpiStatus
    {
        public static int Create = 1;
        public static int Propose = 2;
        public static int Approve = 3;
        public static int Done = 4;
        public static int Canceled = 5;
        public static int NeedEdit = 6;
        public static int Edited = 7;
        public static int Assigned = 8;
        public static int Rejected = 10;
        public static int Evaluating = 9;
        public static int Scored = 11;
        public static int Declared = 12;


        public static Dictionary<int, string> Names = new Dictionary<int, string>()
        {
            { Create, "Tạo mới" },
            { Propose, "Đề xuất" },
            { Approve, "Phê duyệt" },
            { Done, "Hoàn thành" },
            { Canceled, "Bị hủy" },
            { NeedEdit, "Cần bổ sung" },
            { Edited, "Đã chỉnh sửa" },
            { Assigned, "Được giao" },
            { Rejected, "Từ chối kết quả" },
            { Evaluating, "Đang đánh giá" },
            { Scored, "Đã chấm" },
            { Declared, "Đã kê khai" }
        };
    }
}
