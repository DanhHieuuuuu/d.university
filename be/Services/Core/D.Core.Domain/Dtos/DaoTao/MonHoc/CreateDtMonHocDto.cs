using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.DaoTao.MonHoc
{
    public class CreateDtMonHocDto : ICommand
    {
        public string MaMonHoc { get; set; }
        public string TenMonHoc { get; set; }
        public int SoTinChi { get; set; }
        public int? SoTietLyThuyet { get; set; }
        public int? SoTietThucHanh { get; set; }
        public string? MoTa { get; set; }
        public bool TrangThai { get; set; } = true;
        //
        public int? ToBoMonId { get; set; }
    }
}
