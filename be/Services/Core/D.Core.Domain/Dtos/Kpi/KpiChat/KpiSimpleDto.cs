using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Kpi.KpiChat
{
    public class KpiSimpleDto
    {
        public string? DoiTuong { get; set; } 
        public string? TenNguoiSoHuu { get; set; } 
        public string? NoiDungKpi { get; set; }
        public string? MucTieu { get; set; }
        public string? CongThucTinh { get; set; }
        public string? KetQua { get; set; }
        public string? TrongSo { get; set; }
        public string? TrangThai { get; set; }
        public string? NamHoc { get; set; }
    }
}
