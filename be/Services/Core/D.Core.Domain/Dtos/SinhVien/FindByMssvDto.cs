using D.DomainBase.Common;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace D.Core.Domain.Dtos.SinhVien
{
    public class FindByMssvDto : IQuery<SvSinhVienResponseDto>
    {
        private string? _keyword;

        [FromQuery(Name = "keyword"), Description("Mssv")]
        public string? Keyword
        {
            get => _keyword;
            set => _keyword = value?.Trim();
        }
    }
}
