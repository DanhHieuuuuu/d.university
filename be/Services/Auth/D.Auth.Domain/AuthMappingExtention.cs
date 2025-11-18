using AutoMapper;
using D.Auth.Domain.Dtos;
using D.Auth.Domain.Dtos.Login;
using D.Auth.Domain.Dtos.Permission;
using D.Auth.Domain.Dtos.Role;
using D.Auth.Domain.Dtos.User;
using D.Auth.Domain.Entities;

namespace D.Auth.Domain
{
    public class AuthMappingExtention : Profile
    {
        public AuthMappingExtention()
        {
            // Add your mappings here
            // Example: CreateMap<Source, Destination>();

            CreateMap<NsNhanSu, NsNhanSuResponseDto>();

            CreateMap<NsNhanSu, LoginResponseDto>();

            CreateMap<CreateRoleRequestDto, Role>();

            CreateMap<CreatePermissionRequestDto, RolePermission>();

            CreateMap<CreateUserRequestDto, NsNhanSu>();
        }
    }
}
