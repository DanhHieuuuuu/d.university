using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace D.Core.Domain.Entities.SinhVien
{
    [Table(nameof(SvDiemMonHoc), Schema = DbSchema.Sv)]
    public class SvDiemMonHoc : EntityBase
    {
        public int SinhVienId { get; set; }
        public int MonHocId { get; set; }
        public int HocKy { get; set; }
        public string NamHoc { get; set; }
        public decimal DiemQuaTrinh { get; set; }
        public decimal DiemCuoiKy { get; set; }
        public decimal DiemTongKet { get; set; }
        public decimal DiemHe4 { get; set; }
        public string DiemChu { get; set; }
        public string KetQua { get; set; }
        public int LanHoc { get; set; }
        public string GhiChu { get; set; }
    }
}
