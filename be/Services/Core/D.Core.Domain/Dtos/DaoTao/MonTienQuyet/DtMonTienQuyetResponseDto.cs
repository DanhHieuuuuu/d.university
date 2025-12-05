using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.DaoTao.MonTienQuyet
{
    public class DtMonTienQuyetResponseDto
    {
        public int Id { get; set; }
        public int MonHocId { get; set; }
        public int MonTienQuyetId { get; set; }
        public int LoaiDieuKien { get; set; }
        public string? GhiChu { get; set; }
    }
}
