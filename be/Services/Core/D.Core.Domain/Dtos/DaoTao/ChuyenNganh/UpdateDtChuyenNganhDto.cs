using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.DaoTao.ChuyenNganh
{
    public class UpdateDtChuyenNganhDto : ICommand
    {
        public int Id { get; set; }
        public string MaChuyenNganh { get; set; }
        public string TenChuyenNganh { get; set; }
        public string? TenTiengAnh { get; set; }
        public string? MoTa { get; set; }
        public bool? TrangThai { get; set; } = true;
        public int NganhId { get; set; }
    }
}
