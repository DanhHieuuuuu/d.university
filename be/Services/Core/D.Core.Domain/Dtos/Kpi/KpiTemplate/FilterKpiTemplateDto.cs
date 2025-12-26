using D.DomainBase.Common;
using D.DomainBase.Dto;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.Domain.Dtos.Kpi.KpiTemplate
{
    public class FilterKpiTemplateDto : FilterBaseDto, IQuery<PageResultDto<KpiTemplateDto>>
    {
        [FromQuery(Name = "loaiTemplate")]
        public int? LoaiTemplate { get; set; }
        [FromQuery(Name = "loaiKpi")]
        public int? LoaiKpi { get; set; }
        [FromQuery(Name = "namHoc")]
        public string? NamHoc { get; set; }
    }
}
