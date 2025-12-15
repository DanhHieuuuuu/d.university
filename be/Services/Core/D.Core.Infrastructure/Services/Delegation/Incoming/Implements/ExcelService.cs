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
                var cellValue = ws.Cells[1, i + 1].Text.Trim();

                if (!cellValue.Equals(rules[i].Header, StringComparison.OrdinalIgnoreCase))
                {
                    throw new UserFriendlyException(
                        410,
                        $"Sai header cột {i + 1}: mong đợi '{rules[i].Header}', nhận '{cellValue}'"
                    );
                }
            }

            // Kiểm tra data
            for (int row = 2; row <= ws.Dimension.End.Row; row++)
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
                var header = sheet.Cells[1, col].Text.Trim();
                if (!string.IsNullOrEmpty(header))
                    headerMap[header] = col;
            }

            for (int row = 2; row <= sheet.Dimension.End.Row; row++)
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
    }
}
