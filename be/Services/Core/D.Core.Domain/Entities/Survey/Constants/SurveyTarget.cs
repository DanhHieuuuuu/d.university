using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Entities.Survey.Constants
{
    public static class SurveyTarget
    {
        public const int All = 0;
        public const int Student = 1;
        public const int Lecturer = 2;

        public static Dictionary<int, string> Names = new Dictionary<int, string>()
        {
            { All, "Tất cả" },
            { Student, "Sinh viên" },
            { Lecturer, "Giảng viên" },
        };
    }
}
