using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Entities.SinhVien
{
    [Table(nameof(SvKetQuaHocKy), Schema = DbSchema.Sv)]
    public class SvKetQuaHocKy : EntityBase
    {
        public int SinhVienId { get; set; }
        public int HocKy { get; set; }
        public string NamHoc { get; set; }
        public decimal DiemTrungBinhHocKy { get; set; }
        public decimal DiemTrungBinhTinhLuy { get; set; }
        public decimal GpaTichLy { get; set; }
        public int SoTinChiDat { get; set; }
        public int SoTinChiTichLuy { get; set; }
        public int SoTinChiNo { get; set; }
        public string XepLoaiHocKy { get; set; }
        public string XepLoaiRenLuyen { get; set; }
        public int DiemRenLuyen { get; set; }
    }
}
