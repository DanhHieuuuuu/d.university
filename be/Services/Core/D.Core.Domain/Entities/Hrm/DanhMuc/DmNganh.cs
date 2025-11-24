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
    [Table(nameof(DmNganh), Schema = DbSchema.Hrm)]
    public class DmNganh : EntityBase
    {
        public string? MaNganh { get; set; }
        public string? TenNganh { get; set; }
        public string? TenTiengAnh { get; set; }
        public string? MoTa { get; set; }
        public bool? TrangThai { get; set; } = true;
        public virtual ICollection<DmChuyenNganh> ChuyenNganhs { get; set; }
        public virtual ICollection<DmChuongTrinhKhung> ChuongTrinhKhungs { get; set; }
    }
}
