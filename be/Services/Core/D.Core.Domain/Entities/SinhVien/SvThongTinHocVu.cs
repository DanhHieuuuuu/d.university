using D.Core.Domain.Migrations;
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
    [Table(nameof(SvThongTinHocVu), Schema = DbSchema.Sv)]
    public class SvThongTinHocVu : EntityBase
    {
        public int SinhVienId { get; set; }
        public decimal GpaHienTai { get; set; }
        public decimal GpaTBTichLuy { get; set; }
        public string XepLoaiHocLuc { get; set; }
        public int SoTinChiTichLuy { get; set; }
        public int SoTinChiNo { get; set; }
        public bool CanhBaoHocVu { get; set; }
        public int MucCanhBao { get; set; }
        public string LyDoCanhBao { get; set; }
        public int HocKyHienTai { get; set; }
        public string NamHocHienTai { get; set; }
    }
}

