using System.ComponentModel;
using D.DomainBase.Common;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.Domain.Dtos.Hrm.NhanSu
{
    public class FindByMaNsSdtDto : IQuery<NsNhanSuResponseDto>
    {
        private string? _keyword;

        [FromQuery(Name = "keyword"), Description("Mã nhân sự, số điện thoại")]
        public string? Keyword
        {
            get => _keyword;
            set => _keyword = value?.Trim();
        }
    }
}
