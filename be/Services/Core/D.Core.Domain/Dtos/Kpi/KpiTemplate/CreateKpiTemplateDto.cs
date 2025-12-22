using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Kpi.KpiTemplate
{
    public class CreateKpiTemplateDto : ICommand<KpiTemplateDto>
    {
        public string? KPI { get; set; }
        public string? MucTieu { get; set; }
        public string? TrongSo { get; set; }
        public int LoaiKPI { get; set; }
        public int LoaiTemplate { get; set; }
        public string? NamHoc { get; set; }
    }
}
