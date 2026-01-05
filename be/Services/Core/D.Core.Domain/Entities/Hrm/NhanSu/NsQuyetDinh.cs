using System.ComponentModel.DataAnnotations.Schema;
using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;

namespace D.Core.Domain.Entities.Hrm.NhanSu
{
    /// <summary>
    /// Bảng quyết định nhân sự (bổ nhiệm, miễn nhiệm, bãi nhiệm, điều chuyển)
    /// </summary>
    [Table(nameof(NsQuyetDinh), Schema = DbSchema.Hrm)]
    public class NsQuyetDinh : EntityBase
    {
        public int? IdNhanSu { get; set; }
        public string? MaNhanSu { get; set; }
        public int? LoaiQuyetDinh { get; set; }
        public int? Status { get; set; }
        public string? NoiDungTomTat { get; set; }
        public DateTime? NgayHieuLuc { get; set; }
    }
}
