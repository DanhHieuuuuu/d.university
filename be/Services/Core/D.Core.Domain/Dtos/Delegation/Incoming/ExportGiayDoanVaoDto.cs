using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Delegation.Incoming
{
    public class ExportGiayDoanVaoDto : IRequest<ExportFileDto>
    {
        public List<int> ListId { get; set; } = new List<int>();
        public bool IsExportAll { get; set; }
    }
}
