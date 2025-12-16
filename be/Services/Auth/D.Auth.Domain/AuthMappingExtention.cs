using AutoMapper;
using D.Auth.Domain.Dtos;
using D.Auth.Domain.Dtos.Login;
using D.Auth.Domain.Dtos.Permission;
using D.Auth.Domain.Dtos.Role;
using D.Auth.Domain.Dtos.User;
using D.Auth.Domain.Entities;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using D.Core.Domain.Dtos.File;
using D.Core.Domain.Entities.Delegation.Incoming;
using D.Core.Domain.Entities.File;

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

            // File mappings
            CreateMap<FileManagement, FileResponseDto>();
            CreateMap<CreateFileDto, FileManagement>()
                .ForMember(dest => dest.Link, opt => opt.Ignore());
        }
    }
}
