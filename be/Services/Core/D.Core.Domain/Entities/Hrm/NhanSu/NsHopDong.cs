using System.ComponentModel.DataAnnotations.Schema;
using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;

namespace D.Core.Domain.Entities.Hrm.NhanSu
{
    [Table(nameof(NsHopDong), Schema = DbSchema.Hrm)]
    public class NsHopDong : EntityBase
    {
        public string? SoHopDong { get; set; }
        public int? IdNhanSu { get; set; }
        public int? IdLoaiHopDong { get; set; }
        public DateTime? NgayKyKet { get; set; }
        public DateTime? KyLan1 { get; set; }
        public DateTime? KyLan2 { get; set; }
        public DateTime? KyLan3 { get; set; }
        public DateTime? NgayBatDauThuViec { get; set; }
        public DateTime? NgayKetThucThuViec { get; set; }
        public DateTime? HopDongCoThoiHanTuNgay { get; set; }
        public DateTime? HopDongCoThoiHanDenNgay { get; set; }
    }
}
