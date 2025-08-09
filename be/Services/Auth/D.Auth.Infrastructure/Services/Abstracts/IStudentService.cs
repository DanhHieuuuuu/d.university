using D.Auth.Domain.Dtos.students;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Infrastructure.Services.Abstracts
{
    public interface IStudentService
    {
        PageResultDto<StudentResponseDto> GetAll(StudentResquestDto dto);
    }
}
