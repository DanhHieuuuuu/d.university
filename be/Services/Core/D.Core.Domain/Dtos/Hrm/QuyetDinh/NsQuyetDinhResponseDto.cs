namespace D.Core.Domain.Dtos.Hrm.QuyetDinh
{
    public class NsQuyetDinhResponseDto
    {
        public int? Id { get; set; }
        public int? IdNhanSu { get; set; }
        public string? MaNhanSu { get; set; }
        public int? LoaiQuyetDinh { get; set; }
        public int? Status { get; set; }
        public string? NoiDungTomTat { get; set; }
        public DateTime? NgayHieuLuc { get; set; }
    }
}
