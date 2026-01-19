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
    [Table(nameof(DtQuyDinhThangDiem), Schema = DbSchema.Dt)]
    public class DtQuyDinhThangDiem : EntityBase
    {
        public string DiemChu { get; set; }
        public decimal DiemSoMin { get; set; }
        public decimal DiemSoMax { get; set; }
        public decimal DiemHe4 { get; set; }
        public string Mota { get; set; }
        public bool TrangThai { get; set; }
    }
}
