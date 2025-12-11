using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.DaoTao.MonTienQuyet
{
    public class DeleteDtMonTienQuyetDto : ICommand
    {
        public int Id { get; set; }
    }
}
