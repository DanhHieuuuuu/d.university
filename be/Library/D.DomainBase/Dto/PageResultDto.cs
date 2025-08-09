using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.DomainBase.Dto
{
    public class PageResultDto<T>
    {
        public IEnumerable<T>? Items { get; set; }
        public int TotalItem { get; set; }
    }
}
