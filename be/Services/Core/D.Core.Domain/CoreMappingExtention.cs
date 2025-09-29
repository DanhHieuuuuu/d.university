using AutoMapper;
using D.Core.Domain.Dtos.Hrm;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmChucVu;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmPhongBan;
using D.Core.Domain.Dtos.Hrm.NhanSu;
using D.Core.Domain.Dtos.SinhVien;
using D.Core.Domain.Entities.Hrm.DanhMuc;
using D.Core.Domain.Entities.Hrm.NhanSu;
using D.Core.Domain.Entities.SinhVien;
using System.Reflection;

namespace D.Core.Domain
{
    public class CoreMappingExtention : Profile
    {
        public CoreMappingExtention()
        {
            // Add your mappings here
            // Example: CreateMap<Source, Destination>();

            #region hrm

            CreateMap<NsNhanSu, NsNhanSuResponseDto>()
                .ForMember(dest => dest.IdNhanSu, options => options.MapFrom(src => src.Id))
                .ForMember(
                    dest => dest.HoTen,
                    options =>
                        options.MapFrom(src =>
                            string.Join(" ", new[] { src.HoDem, src.Ten }
                            .Where(x => !string.IsNullOrWhiteSpace(x)))
                        )
                );


            CreateMap<CreateNsQuanHeGiaDinhDto, NsQuanHeGiaDinh>();
            CreateMap<CreateNhanSuDto, NsNhanSu>()
                .BeforeMap(
                    (src, dest) =>
                    {
                        TrimAllStringProperties(src);
                    }
                );
            CreateMap<CreateHopDongDto, NsHopDong>();
            CreateMap<DmPhongBan, DmPhongBanResponseDto>();
            CreateMap<DmChucVu, DmChucVuResponseDto>();

            #endregion

            #region sv

            CreateMap<SvSinhVien, SvSinhVienResponseDto>();

            #endregion
        }

        /// <summary>
        /// (Trim) Bỏ khoảng trống 2 đầu với tất cả thuộc tính dạng string
        /// </summary>
        /// <param name="obj"></param>
        private void TrimAllStringProperties(object obj)
        {
            if (obj == null)
                return;

            var stringProperties = obj.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType == typeof(string) && p.CanRead && p.CanWrite);

            foreach (var prop in stringProperties)
            {
                var value = prop.GetValue(obj) as string;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    prop.SetValue(obj, value.Trim());
                }
            }
        }
    }
}
