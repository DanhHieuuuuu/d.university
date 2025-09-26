using System.ComponentModel.DataAnnotations.Schema;
using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;

namespace D.Core.Domain.Entities.Hrm.NhanSu
{
    [Table(nameof(NsQuanHeGiaDinh), Schema = DbSchema.Hrm)]
    public class NsQuanHeGiaDinh : EntityBase
    {
        public int? IdNhanSu { get; set; }
        public int? QuanHe { get; set; }
        public string? HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? QueQuan { get; set; }
        public int? QuocTich { get; set; }
        public string? SoDienThoai { get; set; }
        public string? NgheNghiep { get; set; }
        public string? DonViCongTac { get; set; }
    }
}
