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

        [Description("Khóa học")]
        public string KhoaHoc { get; set; }
        public int TongSoTinChi { get; set; }
        public string? MoTa { get; set; }
        public string? MaNganh { get; set; }
        public bool TrangThai { get; set; } = true;
        public virtual ICollection<DtChuongTrinhKhungMon> ChuongTrinhKhungMons { get; set; }
    }
}
