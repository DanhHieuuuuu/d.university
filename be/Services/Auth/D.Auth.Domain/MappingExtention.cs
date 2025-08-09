using AutoMapper;
using D.Auth.Domain.Dtos.students;
using D.Auth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Domain
{
    public class MappingExtention : Profile
    {
        public MappingExtention()
        {
            // Add your mappings here
            // Example: CreateMap<Source, Destination>();

            CreateMap<Student, StudentResponseDto>();
        }
    }
}
