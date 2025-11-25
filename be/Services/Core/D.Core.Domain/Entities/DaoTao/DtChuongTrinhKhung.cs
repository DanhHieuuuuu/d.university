using D.Core.Domain.Entities.Hrm.DanhMuc;
using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace D.Core.Domain.Entities.DaoTao
{
    [Table(nameof(DtChuongTrinhKhung), Schema = DbSchema.Dt)]
    public class DtChuongTrinhKhung : EntityBase
    {
        public string MaChuongTrinhKhung { get; set; }
        public string TenChuongTrinhKhung { get; set; }
        public int TongSoTinChi { get; set; }
        public string? MoTa { get; set; }
        public bool TrangThai { get; set; } = true;
        //
        public int KhoaHocId { get; set; }
        [ForeignKey(nameof(KhoaHocId))]
        public virtual DmKhoaHoc KhoaHoc { get; set; }

        public int NganhId { get; set; }
        [ForeignKey(nameof(NganhId))]
        public virtual DtNganh Nganh { get; set; }

        public int? ChuyenNganhId { get; set; }
        [ForeignKey(nameof(ChuyenNganhId))]
        public virtual DtChuyenNganh ChuyenNganh { get; set; }

        [InverseProperty(nameof(DtChuongTrinhKhungMon.ChuongTrinhKhung))]
        public virtual ICollection<DtChuongTrinhKhungMon> ChuongTrinhKhungMons { get; set; }
    }
}
