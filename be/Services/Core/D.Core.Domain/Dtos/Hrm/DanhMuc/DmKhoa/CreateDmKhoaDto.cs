using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Hrm.DanhMuc.DmKhoa
{
    public class CreateDmKhoaDto : ICommand
    {
        public string? MaKhoa { get; set; }
        public string? TenKhoa { get; set; }
        public string? Nam { get; set; }
        public string? CachViet { get; set; }
        public int? NguoiTao { get; set; }
        public DateTime? NgayTao { get; set; }
    }
}
