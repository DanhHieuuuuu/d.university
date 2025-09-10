using AutoMapper;
using D.Auth.Domain.Dtos;
using D.Auth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Domain
{
    public class AuthMappingExtention : Profile
    {
        public AuthMappingExtention()
        {
            // Add your mappings here
            // Example: CreateMap<Source, Destination>();

            CreateMap<NsNhanSu, NsNhanSuResponseDto>();
        }
    }
}
