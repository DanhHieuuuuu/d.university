using D.Core.Domain.Entities.Survey.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Surveys
{
    public class SurveyResponseDto
    {
        public int Id { get; set; }
        public string MaKhaoSat { get; set; }
        public string TenKhaoSat { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public int Status { get; set; }
        public string StatusName => SurveyStatus.Names.ContainsKey(Status) ? SurveyStatus.Names[Status] : "Unknown";
        public string? MaYeuCauGoc { get; set; }
    }
}
