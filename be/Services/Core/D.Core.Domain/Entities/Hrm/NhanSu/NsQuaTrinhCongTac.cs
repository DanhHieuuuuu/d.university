using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace D.Core.Domain.Entities.Hrm.NhanSu
{
    [Table(nameof(NsQuaTrinhCongTac), Schema = DbSchema.Hrm)]
    public class NsQuaTrinhCongTac : EntityBase
    {
        public int? IdNhanSu { get; set; }
        public string? MaNhanSu { get; set; }
        public int? IdChucVu { get; set; }
        public int? IdPhongBan { get; set; }
        public int? IdToBoMon { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc {  get; set; }
        public int? IdQuyetDinh { get; set; }
    }
}
