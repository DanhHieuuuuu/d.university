using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace D.Core.Domain.Dtos.Survey.Request
{
    public class CreateRequestSurveyResponseDto
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

        public List<RequestSurveyTargetDto> Targets { get; set; }
        public List<RequestSurveyQuestionDto> Questions { get; set; }
        public List<RequestSurveyCriteriaDto> Criterias { get; set; }
    }
}
