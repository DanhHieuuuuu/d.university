using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace D.Core.Domain.Entities.Hrm.DanhMuc
{
    [Table(nameof(DmQuocTich), Schema = DbSchema.Hrm)]
    public class DmQuocTich : EntityBase
    {
        [Description("Tên viết tắt của quốc gia")]
        public string? MaQuocGia { get; set; }

        [Description("Tên quốc gia (tiếng Việt)")]
        public string? TenQuocGia { get; set; }

        [Description("Tên quốc gia (tiếng Anh")]
        public string? TenQuocGia_EN { get; set; }

        [Description("Thứ tự ưu tiên hiển thị")]
        public int? STT { get; set; }
    }
}
