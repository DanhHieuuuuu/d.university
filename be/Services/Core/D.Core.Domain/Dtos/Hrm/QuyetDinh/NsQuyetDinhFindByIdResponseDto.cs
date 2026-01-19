using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Hrm.QuyetDinh
{
    public class NsQuyetDinhFindByIdResponseDto
    {
        public int? Id { get; set; }
        public int? IdNhanSu { get; set; }
        public string? MaNhanSu { get; set; }
        public string? HoTen { get; set; }
        public int? LoaiQuyetDinh { get; set; }
        public int? Status { get; set; }
        public string? NoiDungTomTat { get; set; }
        public DateTime? NgayHieuLuc { get; set; }
        public List<NsQuyetDinhLogResponseDto>? History { get; set; }
    }
}
