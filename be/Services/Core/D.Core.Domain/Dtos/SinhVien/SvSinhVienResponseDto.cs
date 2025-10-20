using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.SinhVien
{
    public class SvSinhVienResponseDto
    {
        public int? IdSinhVien { get; set; }
        public string? Mssv { get; set; }
        public string? HoDem { get; set; }
        public string? Ten { get; set; }
        //public string? HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? NoiSinh { get; set; }
        public bool? GioiTinh { get; set; }
        public int? QuocTich { get; set; }
        public int? DanToc { get; set; }
        public string? SoCccd { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public int? Khoa { get; set; }
        //public string? TenKhoa { get; set; }
        public string? NganhHoc { get; set; }

        public string HoTen => $"{HoDem} {Ten}".Trim();
    }
}
