using D.ApplicationBase;
using D.Auth.Domain.Dtos.students;
using D.Auth.Domain.Entities;
using D.Auth.Infrastructure.Services.Abstracts;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Application.Query.Studens
{
    public class GetListStudent : IQueryHandler<StudentResquestDto, PageResultDto<StudentResponseDto>>
    {
        public IStudentService _studentService;
        public GetListStudent(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<PageResultDto<StudentResponseDto>> Handle(StudentResquestDto request, CancellationToken cancellationToken)
        {
            return _studentService.GetAll(request);
        }
    }
}
