using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.SinhVien
{
    public class SvThongKeRequestDto : IQuery<SvThongKeResponseDto>
    {
    }

    public class SvThongKeResponseDto
    {
        public int TongSoSinhVien { get; set; }
        public int TongSoMonHoc { get; set; }
        public int TongSoKhoa { get; set; }
    }
}
