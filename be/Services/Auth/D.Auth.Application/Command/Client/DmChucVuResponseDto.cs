using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Application.Command.Client
{
    public class DmChucVuResponseDto
    {
        public int Id { get; set; }
        public string? MaChucVu { get; set; }
        public string? TenChucVu { get; set; }
        public decimal? HsChucVu { get; set; }
        public decimal? HsTrachNhiem { get; set; }
    }
}
