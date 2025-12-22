using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Survey.Request
{
    public class ApproveRequestDto : ICommand
    {
        public int Id { get; set; }
        public ApproveRequestDto(int id) => Id = id;
    }
}
