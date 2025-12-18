using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Request
{
    public class RequestSurveyRequestDto
    {
        public int Id { get; set; }
        public string MaYeuCau { get; set; }
        public string TenKhaoSatYeuCau { get; set; }
        public string MoTa { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public int IdPhongBan { get; set; }
        public int TrangThai { get; set; }
        public string LyDoTuChoi { get; set; }
    }
}
