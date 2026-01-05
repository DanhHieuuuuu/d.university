using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Request
{
    public class CreateRequestSurveyRequestDto : ICommand<CreateRequestSurveyResponseDto>
    {
        public string MaYeuCau { get; set; }
        public string TenKhaoSatYeuCau { get; set; }
        public string MoTa { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public int IdPhongBan { get; set; }

        public List<RequestSurveyTargetDto> Targets { get; set; } = new();
        public List<RequestSurveyQuestionDto> Questions { get; set; } = new();
        public List<RequestSurveyCriteriaDto> Criterias { get; set; } = new();
    }
}
