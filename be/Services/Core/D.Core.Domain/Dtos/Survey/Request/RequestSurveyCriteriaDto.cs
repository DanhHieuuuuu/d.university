using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Request
{
    public class RequestSurveyCriteriaDto
    {
        public int IdTieuChi { get; set; }
        public string TenTieuChi { get; set; }
        public double Weight { get; set; }
        public string Keyword { get; set; }
        public string MoTa { get; set; }
    }
}
