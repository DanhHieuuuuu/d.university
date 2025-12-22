using D.DomainBase.Common;
using D.DomainBase.Dto;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.Domain.Dtos.Kpi.KpiTruong
{
    public class FilterKpiTruongDto : FilterBaseDto, IQuery<PageResultDto<KpiTruongDto>>
    {
        [FromQuery(Name = "loaiKpi")]
        public int? LoaiKpi { get; set; }
        [FromQuery(Name = "namHoc")]
        public string? NamHoc { get; set; }
        [FromQuery(Name = "trangthai")]
        public int? TrangThai { get; set; }
    }
}
