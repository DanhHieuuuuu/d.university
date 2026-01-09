using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Hrm.QuyetDinh
{
    public class CreateNsQuyetDinhDto : ICommand
    {
        public int IdNhanSu { get ; set; }
        public string? MaNhanSu { get; set; }
        public int? LoaiQuyetDinh { get; set; }
        public string? NoiDungTomTat { get; set; }
        public DateTime? NgayHieuLuc { get; set; }
    }
}
