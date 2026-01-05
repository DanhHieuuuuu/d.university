using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Kpi.KpiDonVi
{
    public class CreateKpiDonViDto : ICommand
    {
        public string Kpi { get; set; }
        public string MucTieu { get; set; }
        public string TrongSo { get; set; }
        public int IdDonVi { get; set; }
        public int LoaiKpi { get; set; }
        public string NamHoc { get; set; }
        public int? IdKpiTruong { get; set; }
    }
}
