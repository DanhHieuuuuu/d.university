

using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Hrm.NhanSu
{
    public class CreateNsQuanHeGiaDinhDto
    {
        public int? QuanHe { get; set; }
        public string? HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? QueQuan { get; set; }
        public int? QuocTich { get; set; }
        public string? SoDienThoai { get; set; }
        public string? NgheNghiep { get; set; }
        public string? DonViCongTac { get; set; }
    }
}
