using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.DaoTao.ChuongTrinhKhung
{
    public class DtChuongTrinhKhungResponseDto
    {
        public int Id { get; set; }
        public string MaChuongTrinhKhung { get; set; }
        public string TenChuongTrinhKhung { get; set; }
        public int TongSoTinChi { get; set; }
        public string? MoTa { get; set; }
        public bool TrangThai { get; set; }
        //
        public int KhoaHocId { get; set; }
        public int NganhId { get; set; }
        public int? ChuyenNganhId { get; set; }
    }
}
