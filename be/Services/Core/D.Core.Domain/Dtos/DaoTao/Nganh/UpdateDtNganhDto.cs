using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.DaoTao.Nganh
{
    public class UpdateDtNganhDto : ICommand
    {
        public int Id { get; set; }
        public string? MaNganh { get; set; }
        public string? TenNganh { get; set; }
        public string? TenTiengAnh { get; set; }
        public string? MoTa { get; set; }
        public bool? TrangThai { get; set; } = true;
        public int KhoaId { get; set; }
    }
}
