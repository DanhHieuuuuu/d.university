using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Hrm.NhanSu
{
    public class UpdateNhanSuCongViecDto : ICommand
    {
        public int? IdNhanSu { get; set; }
        public string? MaNhanSu { get; set; }
        public string? MaSoThue { get; set; }
        public string? TenNganHang1 { get; set; }
        public string? Atm1 { get; set; }
        public string? TenNganHang2 { get; set; }
        public string? Atm2 { get; set; }
        public int? HienTaiChucVu { get; set; }
        public int? HienTaiPhongBan { get; set; }
    }
}
