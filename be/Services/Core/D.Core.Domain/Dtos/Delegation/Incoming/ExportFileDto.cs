using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Delegation.Incoming
{
    /// <summary>
    /// Class result cho việc xuất file (tải file trên fe)
    /// </summary>
    public class ExportFileDto
    {
        public Stream? Stream { get; set; }
        public string? FileName { get; set; }
    }
}
