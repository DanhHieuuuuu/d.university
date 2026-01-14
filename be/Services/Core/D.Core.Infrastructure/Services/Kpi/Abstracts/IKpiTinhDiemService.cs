using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Services.Kpi.Abstracts
{
    public interface IKpiTinhDiemService
    {
        Task<decimal> GetDiemTongKetNhanSu(int nhanSuId, string namHoc);
        Task<decimal> GetDiemTongKetNhanSuTrongPhamVi(int nhanSuId, string namHoc, int idDonViQuanLy);
        decimal TinhCongThucChung(IEnumerable<(int? loai, decimal? diem)> dsKpi);
    }
}
