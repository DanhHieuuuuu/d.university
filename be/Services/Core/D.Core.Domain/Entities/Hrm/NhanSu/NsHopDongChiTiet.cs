using System.ComponentModel.DataAnnotations.Schema;
using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using Microsoft.EntityFrameworkCore;

namespace D.Core.Domain.Entities.Hrm.NhanSu
{
    [Table(nameof(NsHopDongChiTiet), Schema = DbSchema.Hrm)]
    public class NsHopDongChiTiet : EntityBase
    {
        public int? IdHopDong { get; set; }
        public int? IdNhanSu { get; set; }
        public string? MaNhanSu { get; set; }
        public int? IdChucVu { get; set; }
        public int? IdPhongBan { get; set; }
        public int? IdToBoMon { get; set; }
        public int? LuongCoBan { get; set; }

        [Precision(4, 2)]
        public decimal? HsChucVu { get; set; }

        [Precision(4, 2)]
        public decimal? HsLuong { get; set; }

        [Precision(4, 2)]
        public decimal? HsKhac { get; set; }
        public string? GhiChu { get; set; }
    }
}
