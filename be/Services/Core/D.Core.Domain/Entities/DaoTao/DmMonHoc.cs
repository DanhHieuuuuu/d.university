using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace D.Core.Domain.Entities.DaoTao
{
    [Table(nameof(DtMonHoc), Schema = DbSchema.Dt)]
    public class DtMonHoc : EntityBase
    {
        public string MaMonHoc { get; set; }
        public string TenMonHoc { get; set; }
        public int SoTinChi { get; set; }
        public int? SoTietLyThuyet { get; set; }
        public int? SoTietThucHanh { get; set; }
        public string? MoTa { get; set; }
        public bool TrangThai { get; set; } = true;
        public int? ToBoMonId { get; set; }

        public virtual ICollection<DtChuongTrinhKhungMon> ChuongTrinhKhungMons { get; set; }
        public virtual ICollection<DtMonTienQuyet> MonTienQuyet { get; set; }
        public virtual ICollection<DtMonTienQuyet> LaTienQuyetMon { get; set; }
    }
}
