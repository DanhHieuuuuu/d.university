using D.DomainBase.Common;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Domain.Dtos.students
{
    public class StudentResquestDto : FilterBaseDto, IQuery<PageResultDto<StudentResponseDto>>
    {
    }
}
