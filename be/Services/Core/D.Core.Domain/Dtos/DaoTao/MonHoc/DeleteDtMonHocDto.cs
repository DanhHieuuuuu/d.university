using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.DaoTao.MonHoc
{
    public class DeleteDtMonHocDto : ICommand
    {
        public int Id { get; set; }
    }
}
