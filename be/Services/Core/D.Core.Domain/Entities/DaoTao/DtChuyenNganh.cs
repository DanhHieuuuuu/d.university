using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Entities.DaoTao
{
    [Table(nameof(DtChuyenNganh), Schema = DbSchema.Hrm)]
    public class DtChuyenNganh : EntityBase
    {
        public string MaChuyenNganh { get; set; }
        public string TenChuyenNganh { get; set; }
        public string? TenTiengAnh { get; set; }
        public string? MoTa { get; set; }
        public bool? TrangThai { get; set; } = true;
        public string MaNganh { get; set; }
        [ForeignKey(nameof(MaNganh))]
        public virtual DtNganh Nganh { get; set; }
        public virtual ICollection<DtChuongTrinhKhung> ChuongTrinhKhungs { get; set; }
    }
}
