using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Request
{
    public class RequestSurveyTargetDto
    {
        public int LoaiDoiTuong { get; set; }
        public int? IdPhongBan { get; set; }
        public int? IdKhoa { get; set; }
        public int? IdKhoaHoc { get; set; }
        public string MoTa { get; set; }
    }
}
