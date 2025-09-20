using AutoMapper;
using D.Core.Domain.Dtos.Hrm;
using D.Core.Domain.Dtos.SinhVien;
using D.Core.Domain.Entities.Hrm.NhanSu;
using D.Core.Domain.Entities.SinhVien;

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

            #region sv

            CreateMap<SvSinhVien, SvSinhVienResponseDto>();

            #endregion
        }
    }
}
