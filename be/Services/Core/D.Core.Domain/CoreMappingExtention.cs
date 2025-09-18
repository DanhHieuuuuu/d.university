using AutoMapper;
using D.Core.Domain.Dtos.Hrm;
using D.Core.Domain.Entities.Hrm.NhanSu;

namespace D.Core.Domain
{
    public class CoreMappingExtention : Profile
    {
        public CoreMappingExtention()
        {
            // Add your mappings here
            // Example: CreateMap<Source, Destination>();

            #region hrm

            CreateMap<NsNhanSu, NsNhanSuResponseDto>();

            #endregion
        }
    }
}
