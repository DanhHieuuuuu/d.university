using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.AI
{
    public class AIReportDetailDto
    {
        public int Id { get; set; }
        public int IdBaoCao { get; set; }
        public int IdTieuChi { get; set; }
        public double DiemCamXuc { get; set; }
        public string NhanCamXuc { get; set; }
        public string TomTatNoiDung { get; set; }
        public string XuHuong { get; set; }
        public string GoiYCaiThien { get; set; }

        public string TenTieuChi { get; set; }
        public double Weight { get; set; }
    }
}
