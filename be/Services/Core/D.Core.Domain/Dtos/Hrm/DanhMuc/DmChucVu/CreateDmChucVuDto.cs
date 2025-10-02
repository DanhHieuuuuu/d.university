using D.DomainBase.Common;
using Microsoft.EntityFrameworkCore;

namespace D.Core.Domain.Dtos.Hrm.DanhMuc.DmChucVu
{
    public class CreateDmChucVuDto : ICommand
    {
        public string? MaChucVu { get; set; }
        public string? TenChucVu { get; set; }

        [Precision(4, 2)]
        public decimal? HsChucVu { get; set; }

        [Precision(4, 2)]
        public decimal? HsTrachNhiem { get; set; }
    }
}
