using AutoMapper;
using D.Auth.Domain.Dtos.students;
using D.Auth.Infrastructure.Repositories;
using D.Auth.Infrastructure.Services.Abstracts;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Infrastructure.Services.Implements
{
    public class StudentService : ServiceBase, IStudentService
    {
        private readonly ServiceUnitOfWork _unitOfWork;

        public StudentService(
            ILogger<StudentService> logger,
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork
        )
            : base(logger, contextAccessor, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public PageResultDto<StudentResponseDto> GetAll(StudentResquestDto dto)
        {
            _logger.LogInformation($"{nameof(GetAll)} method called. Dto: {dto}");

            var query = _unitOfWork.iStudentRepository.TableNoTracking.Where(x => string.IsNullOrEmpty(dto.Keyword) || dto.Keyword == x.Name);
            var result = _mapper.Map<List<StudentResponseDto>>(query);
            return new PageResultDto<StudentResponseDto>
            {
                Items = result.Skip(dto.SkipCount()).Take(dto.PageSize).ToList(),
                TotalItem = result.Count(),
            };
        }
    }
}
