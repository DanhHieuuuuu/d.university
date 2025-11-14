using D.Core.Domain.Dtos.SinhVien;
using D.DomainBase.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.File
{
    public class CreateFileDto : ICommand<FileResponseDto>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IFormFile? File { get; set; }
        public string? ApplicationField { get; set; }
    }
}
