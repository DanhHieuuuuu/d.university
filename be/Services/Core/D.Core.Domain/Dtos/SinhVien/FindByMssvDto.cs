using D.DomainBase.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
