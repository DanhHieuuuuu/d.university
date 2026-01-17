using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Logging
{
    public class LogSurveyResponseDto
    {
        public int Id { get; set; }
        public int? IdNguoiThaoTac { get; set; }
        public string? TenNguoiThaoTac { get; set; }
        public string LoaiHanhDong { get; set; }
        public string MoTa { get; set; }
        public string TenBang { get; set; }
        public string IdDoiTuong { get; set; }
        public string? DuLieuCu { get; set; }
        public string? DuLieuMoi { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
