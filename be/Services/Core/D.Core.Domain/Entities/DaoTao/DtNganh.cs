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
    [Table(nameof(DtNganh), Schema = DbSchema.Dt)]
    public class DtNganh : EntityBase
    {
        public string? MaNganh { get; set; }
        public string? TenNganh { get; set; }
        public string? TenTiengAnh { get; set; }
        public string? MoTa { get; set; }
        public bool? TrangThai { get; set; } = true;
        //
        public int KhoaId { get; set; }

        [ForeignKey(nameof(KhoaId))]
        public virtual DtKhoa Khoa { get; set; }

        [InverseProperty(nameof(DtChuyenNganh.Nganh))]
        public virtual ICollection<DtChuyenNganh> ChuyenNganhs { get; set; }

        [InverseProperty(nameof(DtChuongTrinhKhung.Nganh))]
        public virtual ICollection<DtChuongTrinhKhung> ChuongTrinhKhungs { get; set; }
    }
}
