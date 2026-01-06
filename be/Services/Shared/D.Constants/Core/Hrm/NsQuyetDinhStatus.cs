namespace D.Constants.Core.Hrm
{
    public static class NsQuyetDinhStatus
    {
        public static int TaoMoi = 0;
        public static int PheDuyet = 1;
        public static int TuChoi = 2;

        public static Dictionary<int, string> Names = new Dictionary<int, string>()
        {
            { TaoMoi, "Tạo mới" },
            { PheDuyet, "Đã phê duyệt" },
            { TuChoi, "Từ chối" },
        };
    }
}
