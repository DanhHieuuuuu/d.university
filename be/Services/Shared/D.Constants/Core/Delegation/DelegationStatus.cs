
namespace D.Constants.Core.Delegation
{
    public class DelegationStatus
    {
        public static int Create = 1;
        public static int Propose = 2;
        public static int BGHApprove = 3;
        public static int ReceptionGroup = 4;
        public static int Done = 5;
        public static int Canceled = 6;
        public static int NeedEdit = 7;
        public static int Edited = 8;

        public static Dictionary<int, string> Names = new Dictionary<int, string>()
        {
            { Create, "Tạo mới" },
            { Propose, "Đề xuất" },
            { BGHApprove, "BGH phê duyệt" },
            { ReceptionGroup, "Đang thực hiên tiếp đoàn" },
            { Done, "Hoàn thành" },
            { Canceled, "Bị hủy" },
            { NeedEdit, "Cần bổ sung" },
            { Edited, "Đã chỉnh sửa" },
        };
    }
}
