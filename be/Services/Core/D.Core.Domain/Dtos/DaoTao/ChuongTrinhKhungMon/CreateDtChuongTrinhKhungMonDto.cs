using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.DaoTao.ChuongTrinhKhungMon
{
    public class CreateDtChuongTrinhKhungMonDto : ICommand
    {
        public int ChuongTrinhKhungId { get; set; }
        public int MonHocId { get; set; }
        public string? HocKy { get; set; }
        public string? NamHoc { get; set; }
        public bool TrangThai { get; set; } = true;
    }
}
