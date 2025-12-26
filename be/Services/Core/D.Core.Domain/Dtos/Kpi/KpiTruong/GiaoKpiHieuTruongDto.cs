using D.DomainBase.Common;
using D.DomainBase.Dto;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.Domain.Dtos.Kpi.KpiTruong
{
    public class GiaoKpiHieuTruongDto : ICommand
    {
        public string? NamHoc { get; set; }
    }
}
