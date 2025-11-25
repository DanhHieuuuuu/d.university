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
    [Table(nameof(DtKhoa), Schema = DbSchema.Dt)]
    public class DtKhoa : EntityBase
    {
        public string? MaKhoa { get; set; }
        public string? TenKhoa { get; set; }
        public string? TenTiengAnh { get; set; }
        public string? VietTat { get; set; }
        public string? Email { get; set; }
        public string? Sdt { get; set; }
        public string? DiaChi { get; set; }
        public bool? TrangThai { get; set; } = true;
        //
        [InverseProperty(nameof(DtNganh.Khoa))]
        public virtual ICollection<DtNganh> Nganhs { get; set; }
    }
}
