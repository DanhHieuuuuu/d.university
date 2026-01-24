namespace D.Core.Domain.Dtos.Hrm.SemanticSearch
{
    public class SearchSemanticResponseDto
    {
        public int? IdNhanSu { get; set; }
        public string? MaNhanSu { get; set; }

        // Thông tin cá nhân
        public string? HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? NgaySinhText { get; set; }
        public string? NoiSinh { get; set; }
        public int? GioiTinh { get; set; }
        public string? GioiTinhText { get; set; }
        public int? QuocTich { get; set; }
        public string? TenQuocTich { get; set; }
        public int? DanToc { get; set; }
        public string? TenDanToc { get; set; }
        public int? TonGiao { get; set; }
        public string? NguyenQuan { get; set; }
        public string? NoiOHienTai { get; set; }
        public string? SoCccd { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }

        // Thông tin công việc
        public string? TenChucVu { get; set; }
        public string? TenPhongBan { get; set; }
        public string? TenToBoMon { get; set; }

        // Thông tin học vấn
        public string? TrinhDoHocVan { get; set; }
        public string? TrinhDoNgoaiNgu { get; set; }
        public string? TenHocVi { get; set; }
        public string? TenChuyenNganhHocVi { get; set; }
        public string? TenHocHam { get; set; }
        public string? TenChuyenNganhHocHam { get; set; }
    }
}
