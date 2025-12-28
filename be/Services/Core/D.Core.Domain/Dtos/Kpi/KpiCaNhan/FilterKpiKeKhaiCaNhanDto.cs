using D.DomainBase.Common;
using D.DomainBase.Dto;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.Domain.Dtos.Kpi.KpiCaNhan
{
    public class FilterKpiKeKhaiCaNhanDto : FilterBaseDto, IQuery<PageResultDto<KpiCaNhanDto>>
    {
        [FromQuery(Name = "idNhanSu")]
        public int? IdNhanSu { get; set; }
        [FromQuery(Name = "loaiKpi")]
        public int? LoaiKpi { get; set; }
        [FromQuery(Name = "namHoc")]
        public string? NamHoc { get; set; }
        [FromQuery(Name = "IdPhongBan")]
        public int? IdPhongBan { get; set; }
        [FromQuery(Name = "trangthai")]
        public int? TrangThai { get; set; }
    }
}
