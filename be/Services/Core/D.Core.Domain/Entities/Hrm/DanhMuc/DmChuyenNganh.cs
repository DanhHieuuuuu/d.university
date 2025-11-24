using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Entities.Hrm.DanhMuc
{
    [Table(nameof(DmChuyenNganh), Schema = DbSchema.Hrm)]
    public class DmChuyenNganh : EntityBase
    {
        public string MaChuyenNganh { get; set; }
        public string TenChuyenNganh { get; set; }
        public string? TenTiengAnh { get; set; }
        public string? MoTa { get; set; }
        public bool? TrangThai { get; set; } = true;
        public string MaNganh { get; set; }
        [ForeignKey(nameof(MaNganh))]
        public virtual DmNganh Nganh { get; set; }
        public virtual ICollection<DmChuongTrinhKhung> ChuongTrinhKhungs { get; set; }
    }
}
