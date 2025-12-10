
namespace D.Constants.Core.Delegation
{
    public class DelegationStatus
    {
        public static int Create = 1;
        public static int Edited = 2;
        public static int Canceled = 3;

        public static Dictionary<int, string> Names = new Dictionary<int, string>()
        {
            { Create, "Tạo mới" },           
            { Canceled, "Bị hủy" },            
            { Edited, "Đã chỉnh sửa" },
         
        };
    }
}
