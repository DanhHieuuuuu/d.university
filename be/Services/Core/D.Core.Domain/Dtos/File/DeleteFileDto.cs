using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.File
{
    public class DeleteFileDto : ICommand<bool>
    {
        public int Id { get; set; }
    }
}