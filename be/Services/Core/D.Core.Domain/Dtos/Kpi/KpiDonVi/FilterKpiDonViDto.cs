using D.DomainBase.Common;
using D.DomainBase.Dto;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.Domain.Dtos.Kpi.KpiDonVi
{
    public class FilterKpiDonViDto : FilterBaseDto, IQuery<PageResultDto<KpiDonViDto>>
    {
        [FromQuery(Name = "idDonVi")]
        public int? IdDonVi { get; set; }
        [FromQuery(Name = "loaiKpi")]
        public int? LoaiKpi { get; set; }
        [FromQuery(Name = "namHoc")]
        public string? NamHoc { get; set; }
        [FromQuery(Name = "trangThai")]
        public int? TrangThai { get; set; }
    }
}
