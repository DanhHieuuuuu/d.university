namespace D.Constants.Core.Hrm
{
    public static class NsLoaiQuyetDinh
    {
        /// Thêm nhân viên mới
        public static int TiepNhan = 1;

        // Điều chuyển nhân viên sang phòng ban khác
        public static int DieuChuyen = 2;

        // Bổ nhiệm trưởng phòng/phó phòng/trưởng khoa/...
        public static int BoNhiem = 3;

        // Bãi nhiệm trưởng phòng/trưởng khoa/phó khoa/...
        public static int BaiNhiem = 4;

        // Quyết định cho thôi việc nhân viên
        public static int ThoiViec = 5;

        public static Dictionary<int, string> Names = new Dictionary<int, string>()
        {
            { TiepNhan, "tiếp nhận" },
            { DieuChuyen, "điều chuyển" },
            { BoNhiem, "bổ nhiệm" },
            { BaiNhiem, "bãi nhiệm" },
            { ThoiViec, "thôi việc" },
        };
    }
}
