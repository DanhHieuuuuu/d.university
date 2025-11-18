using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.SinhVien
{
    public class CreateSinhVienDto : ICommand<SvSinhVienResponseDto>
    {
        public string? HoDem { get; set; }
        public string? Ten { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? NoiSinh { get; set; }
        public bool? GioiTinh { get; set; }
        public int? QuocTich { get; set; }
        public int? DanToc { get; set; }
        public string? SoCccd { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        //public int? KhoaHoc { get; set; }
        public int? Khoa { get; set; }
        public int? ChuyenNganh { get; set; }
    }
}
