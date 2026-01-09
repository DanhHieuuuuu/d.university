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
