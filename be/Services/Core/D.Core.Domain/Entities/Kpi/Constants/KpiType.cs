namespace D.Core.Domain.Entities.Kpi.Constants
{
    public class KpiTypes
    {
        public static int Functional = 1;
        public static int Objective = 2;
        public static int Compliance = 3;



        public static Dictionary<int, string> Names = new Dictionary<int, string>()
        {
            { Functional, "Chức năng" },
            { Objective, "Mục tiêu" },
            { Compliance, "Tuân thủ" },
        };
    }
}
