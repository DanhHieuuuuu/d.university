using D.ControllerBase.Exceptions;
using D.Core.Domain.Dtos.Delegation;
using D.Core.Domain.Entities.Delegation.Incoming;
using D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;

namespace D.Core.Infrastructure.Services.Delegation.Incoming.Implements
{
    public class ExcelService : IExcelService
    {
        /// <summary>
        /// Kiểm tra data có đúng định dạng hay không
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="rules"></param>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task CheckValidateDetailDelegationAsync(
            IFormFile file,
            List<ExcelColumnRule> rules
        )
        {
            ExcelPackage.License.SetNonCommercialPersonal("Hieu_Nguyen");
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);

            using var package = new ExcelPackage(stream);
            var ws = package.Workbook.Worksheets["Danh sách đoàn vào"];

            // Kiểm tra header
            for (int i = 0; i < rules.Count; i++)
            {
                var cellValue = ws.Cells[2, i + 1].Text.Trim();

                if (!cellValue.Equals(rules[i].Header, StringComparison.OrdinalIgnoreCase))
                {
                    throw new UserFriendlyException(
                        410,
                        $"Sai header cột {i + 1}: mong đợi '{rules[i].Header}', nhận '{cellValue}'"
                    );
                }
            }

            // Kiểm tra data
            for (int row = 3; row <= ws.Dimension.End.Row; row++)
            {
                for (int col = 0; col < rules.Count; col++)
                {
                    var rule = rules[col];
                    var value = ws.Cells[row, col + 1].Text.Trim();

                    if (rule.Required && string.IsNullOrEmpty(value))
                    {
                        throw new UserFriendlyException(
                            411,
                            $"Dòng {row}: {rule.Header} không được để trống"
                        );
                    }

                    if (
                        !string.IsNullOrEmpty(value)
                        && rule.Validator != null
                        && !rule.Validator(value)
                    )
                    {
                        throw new UserFriendlyException(412, $"Dòng {row}: {rule.ErrorMessage}");
                    }
                }
            }
        }

        public async Task<List<DetailDelegationIncoming>> ParseExcelToListDetailDelegationAsync(
            IFormFile file
        )
        {
            if (file == null || file.Length == 0)
                throw new Exception("File Excel rỗng");

            var result = new List<DetailDelegationIncoming>();

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            stream.Position = 0;

            using var package = new ExcelPackage(stream);
            var sheet = package.Workbook.Worksheets[0];

            if (sheet?.Dimension == null)
                return result;

            // Map header → column index
            var headerMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            for (int col = 1; col <= sheet.Dimension.End.Column; col++)
            {
                var header = sheet.Cells[2, col].Text.Trim();
                if (!string.IsNullOrEmpty(header))
                    headerMap[header] = col;
            }

            for (int row = 3; row <= sheet.Dimension.End.Row; row++)
            {
                // Skip row trống
                if (sheet.Cells[row, 1].Text.Trim() == string.Empty)
                    continue;

                var item = new DetailDelegationIncoming
                {
                    FirstName = GetCell(sheet, row, headerMap, "Họ"),
                    LastName = GetCell(sheet, row, headerMap, "Tên"),
                    PhoneNumber = GetCell(sheet, row, headerMap, "Số điện thoại liên lạc"),
                    Email = GetCell(sheet, row, headerMap, "Email"),
                    IsLeader = GetCell(sheet, row, headerMap, "Trưởng đoàn (Có/Không)")
                        .Equals("Có", StringComparison.OrdinalIgnoreCase),
                };

                var yearText = GetCell(sheet, row, headerMap, "Năm sinh");
                item.YearOfBirth = int.TryParse(yearText, out var year) ? year : 0;

                result.Add(item);
            }

            return result;
        }

        private static string GetCell(
            ExcelWorksheet sheet,
            int row,
            Dictionary<string, int> headerMap,
            string header
        )
        {
            if (!headerMap.TryGetValue(header, out var col))
                return string.Empty;

            return sheet.Cells[row, col].Text?.Trim() ?? string.Empty;
        }

        public async Task<byte[]> ExportAsync<T>(List<T> data,string sheetName,string title)
        {
            if (data == null || !data.Any())
                throw new UserFriendlyException(4004, "Không có dữ liệu để xuất Excel");

            ExcelPackage.License.SetNonCommercialPersonal("Hieu_Nguyen");

            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add(sheetName);

            var properties = typeof(T).GetProperties();

            // ===== TITLE =====
            ws.Cells[1, 1].Value = title;
            ws.Cells[1, 1, 1, properties.Length].Merge = true;
            ws.Cells[1, 1].Style.Font.Bold = true;
            ws.Cells[1, 1].Style.Font.Size = 16;
            ws.Cells[1, 1].Style.HorizontalAlignment =
                OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

            // ===== HEADER =====
            for (int i = 0; i < properties.Length; i++)
            {
                ws.Cells[3, i + 1].Value = properties[i].Name;
                ws.Cells[3, i + 1].Style.Font.Bold = true;
                ws.Cells[3, i + 1].Style.Border.BorderAround(
                    OfficeOpenXml.Style.ExcelBorderStyle.Thin
                );
            }

            // ===== DATA =====
            for (int r = 0; r < data.Count; r++)
            {
                for (int c = 0; c < properties.Length; c++)
                {
                    ws.Cells[r + 4, c + 1].Value =
                        properties[c].GetValue(data[r]);
                }
            }

            ws.Cells.AutoFitColumns();

            return await Task.FromResult(package.GetAsByteArray());
        }

    }
}
