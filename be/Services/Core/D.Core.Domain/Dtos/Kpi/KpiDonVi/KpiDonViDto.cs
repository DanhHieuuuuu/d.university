using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Kpi.KpiDonVi
{
    public class KpiDonViDto
    {
        public int Id { get; set; }
        public int? STT { get; set; }
        public string? Kpi { get; set; }
        public string? MucTieu { get; set; }
        public string? TrongSo { get; set; }
        public string? DonVi { get; set; }
        public int? IdDonVi { get; set; }
        public int? LoaiKpi { get; set; }
        public int? IdKpiTruong { get; set; }
        public string? NamHoc { get; set; }
        public int? TrangThai { get; set; }
        public decimal? KetQuaThucTe { get; set; }
        public decimal? CapTrenDanhGia { get; set; }
        public decimal? DiemKpi { get; set; }
        public decimal? DiemKpiCapTren { get; set; }
        public string? LoaiKetQua { get; set; }
        public string? GhiChu { get; set; }
        public int? IsActive { get; set; }
        public string? CongThuc { get; set; }
        public int? IdCongThuc { get; set; }
    }
}
