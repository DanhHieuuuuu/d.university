using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.DaoTao.Khoa
{
    public class DtKhoaResponseDto
    {
        public int Id { get; set; }
        public string? MaKhoa { get; set; }
        public string? TenKhoa { get; set; }
        public string? TenTiengAnh { get; set; }
        public string? VietTat { get; set; }
        public string? Email { get; set; }
        public string? Sdt { get; set; }
        public string? DiaChi { get; set; }
        public bool? TrangThai { get; set; }
    }
}
