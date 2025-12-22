using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Kpi.KpiTemplate
{
    public class KpiTemplateDto
    {
        public int Id { get; set; }
        public string? KPI { get; set; }
        public string? MucTieu { get; set; }
        public string? TrongSo { get; set; }
        public int LoaiKPI { get; set; }
        public int LoaiTemplate { get; set; }

        public string? LoaiTemplateName { get; set; }
        public string? NamHoc { get; set; }
    }
}
