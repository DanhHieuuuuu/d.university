using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Services.Kpi.Common
{
    public class TinhDiemKPI
    {
        public static decimal ParseNumberLoose(object? input)
        {
            if (input == null)
                return 0;

            var str = input
                .ToString()!
                .Trim()
                .Replace("%", "")
                .Replace("<=", "")
                .Replace(">=", "")
                .Replace("<", "")
                .Replace(">", "")
                .Replace("giây", "")
                .Replace("nghiệp vụ/ đơn vị", "")
                .Replace("lần", "")
                .Replace("nghiệp vụ", "")
                .Replace("cuộc", "")
                .Replace("hội chợ/năm", "")
                .Replace("sản phẩm", "")
                .Replace("chiến dịch", "")
                .Replace("bài/tin", "")
                .Replace("chương trình", "")
                .Replace("đơn vị", "")
                .Replace("/ đơn vị", "")
                .Replace("người", "")
                .Replace("SV", "")
                .Trim();

            return decimal.TryParse(
                str,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out var val
            )
                ? val
                : 0;
        }

        public static decimal GetPhanTramTruTuanThu(string? tenKpi, decimal ketQua)
        {
            if (string.IsNullOrEmpty(tenKpi)) return 0;
            var name = tenKpi.ToLower();

            // 1. Vi phạm về thời gian làm việc
            if (name.Contains("thời gian làm việc"))
                return ketQua > 7 ? 30 : (ketQua == 6 ? 10 : (ketQua == 5 ? 5 : (ketQua == 4 ? 2 : (ketQua == 3 ? 1 : 0))));

            // 2. Vi phạm về quy định chấm công
            if (name.Contains("chấm công"))
                return ketQua > 8 ? 30 : (ketQua == 7 ? 10 : (ketQua == 6 ? 5 : (ketQua == 5 ? 2 : (ketQua == 4 ? 1 : 0))));

            // 3, 4, 5. Trật tự tác phong / Quy tắc ứng xử / Đồng phục
            if (name.Contains("trật tự") || name.Contains("ứng xử") || name.Contains("đồng phục"))
                return ketQua > 3 ? 30 : (ketQua == 3 ? 10 : (ketQua == 2 ? 5 : (ketQua == 1 ? 2 : 0)));

            // 6. Vi phạm quy trình nghiệp vụ (Quy ước: 4=Văn bản >=2, 3=Văn bản 1, 2=Lời nói 2, 1=Lời nói 1)
            if (name.Contains("quy trình nghiệp vụ"))
                return ketQua >= 4 ? 30 : (ketQua == 3 ? 10 : (ketQua == 2 ? 5 : (ketQua == 1 ? 2 : 0)));
            return 0;
        }

        public static decimal GetDiemTruTuanThuDonVi(string? tenKpi, decimal tyLeViPhamThucTe)
        {
            if (string.IsNullOrEmpty(tenKpi)) return 0;
            var name = tenKpi.ToLower().Trim();

            // Mục tiêu <= 5%, Trừ 2% cho mỗi 1% vượt
            if (name.Contains("thời gian làm việc") || name.Contains("chấm công"))
            {
                // Nếu nhỏ hơn hoặc bằng 5% thì không bị trừ
                if (tyLeViPhamThucTe <= 5) return 0;

                // Công thức: (Thực tế - 5) * 2
                return (tyLeViPhamThucTe - 5) * 2;
            }

            // Mục tiêu <= 2%, Trừ 5% cho mỗi 1% vượt
            if (name.Contains("trật tự") ||
                name.Contains("ứng xử") ||
                name.Contains("đồng phục") ||
                name.Contains("thẻ nhân viên") ||
                name.Contains("quy trình nghiệp vụ"))
            {
                // Nếu nhỏ hơn hoặc bằng 2% thì không bị trừ
                if (tyLeViPhamThucTe <= 2) return 0;

                // Công thức: (Thực tế - 2) * 5
                return (tyLeViPhamThucTe - 2) * 5;
            }

            return 0;
        }
        public static decimal TinhDiemChung(
            object? ketQua,
            string? mucTieu,
            string? trongSo,
            int? idCongThuc,
            string? loaiKetQua
        )
        {
            var ts = ParseNumberLoose(trongSo);
            var kq = ParseNumberLoose(ketQua);
            var mt = ParseNumberLoose(mucTieu);
            if (ts <= 0)
                return 0;
            if (idCongThuc == 11)
            {
                var kq11 = ParseNumberLoose(ketQua);

                if (loaiKetQua == "YES_NO")
                    return kq11 == 1 ? ts : 0;

                if (loaiKetQua == "PERCENT")
                    return kq11 <= mt ? ts : 0;
                if (loaiKetQua == "NUMBER")
                    return kq11 >= mt ? ts : 0;
            }

            if (mt < 0)
                return 0;

            var max = 1.2m * ts;

            return idCongThuc switch
            {
                1 => Math.Min(max, Math.Max(0, ts - kq)),
                2 => Math.Min(max, Math.Max(0, ts - 2 * kq)),
                3 => mt == 0 ? 0 : Math.Min(max, Math.Max(0, kq / mt * ts)),
                4 => kq == 0 ? 0 : Math.Min(max, Math.Max(0, mt / kq * ts)),
                5 => kq <= mt ? ts : 0,
                6 => kq >= mt ? ts : 0,
                7 => kq >= mt ? Math.Min(max, Math.Max(0, kq / mt * ts)) : 0,
                8 => kq > mt ? Math.Min(max, Math.Max(0, mt / kq * ts)) : ts,
                9 => kq <= mt ? ts : Math.Min(max, Math.Max(0, ts - kq)),
                10 => kq <= mt ? ts : Math.Min(max, Math.Max(0, ts - 3 * kq)),
                12 => kq > mt ? Math.Min(max, Math.Max(0, ts - (kq - mt) * 0.1m * ts)) : ts,
                13 => kq > mt ? Math.Min(max, Math.Max(0, ts - (kq - mt) * 0.3m * ts)) : ts,
                14 => Math.Max(0, ts - 0.1m * kq * ts),
                15 => Math.Min(max, Math.Max(0, ts - 0.2m * kq * ts)),
                _ => 0
            };
        }
    }

}
