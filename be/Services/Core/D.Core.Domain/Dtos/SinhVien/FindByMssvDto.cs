using D.DomainBase.Common;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace D.Core.Domain.Dtos.SinhVien
{
    public class FindByMssvDto : IQuery<SvSinhVienResponseDto>
    {
        private string? _mssv;

        [FromQuery(Name = "mssv")]
        public string? Mssv
        {
            get => _mssv;
            set => _mssv = value?.Trim();
        }
    }
}
