using D.Core.Domain.Dtos.Hrm.QuanHeGiaDinh;
using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Hrm.NhanSu
{
    public class CreateNhanSuDto : ICommand<NsNhanSuResponseDto>
    {
        // Thông tin cá nhân
        public string? HoDem { get; set; }
        public string? Ten { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? NoiSinh { get; set; }
        public int? GioiTinh { get; set; }
        public int? QuocTich { get; set; }
        public int? DanToc { get; set; }
        public int? TonGiao { get; set; }
        public string? NguyenQuan { get; set; }
        public string? NoiOHienTai { get; set; }
        public string? SoCccd { get; set; }
        public DateTime? NgayCapCccd { get; set; }
        public string? NoiCapCccd { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? KhanCapSoDienThoai { get; set; }
        public string? KhanCapNguoiLienHe { get; set; }
        public List<CreateNsQuanHeGiaDinhDto>? ThongTinGiaDinh { get; set; }        

        // Thông tin sức khỏe
        public decimal? ChieuCao { get; set; }
        public decimal? CanNang { get; set; }
        public string? NhomMau { get; set; }
    }
}
