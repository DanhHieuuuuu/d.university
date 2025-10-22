using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.SinhVien
{
    public class DeleteSinhVienDto : ICommand<bool>
    {
        public string? Mssv { get; set; }
    }
}
