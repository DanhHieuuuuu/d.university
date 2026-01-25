using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Hrm
{
    public class ThongKeNhanSuTheoPhongBanResponseDto
    {
        public int Id { get; set; }
        public int? STT { get; set; }
        public string? TenPhongBan { get; set; }
        public int SoLuongNhanSu { get; set; }
    }
}
