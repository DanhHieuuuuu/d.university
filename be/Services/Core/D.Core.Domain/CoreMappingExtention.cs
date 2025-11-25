using System.Reflection;
using AutoMapper;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming.Paging;
using D.Core.Domain.Dtos.File;
using D.Core.Domain.Dtos.Hrm;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmChucVu;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmDanToc;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmGioiTinh;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmKhoaHoc;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmLoaiHopDong;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmPhongBan;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmQuanHeGiaDinh;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmQuocTich;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmToBoMon;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmTonGiao;
using D.Core.Domain.Dtos.Hrm.NhanSu;
using D.Core.Domain.Dtos.SinhVien;
using D.Core.Domain.Entities.Delegation.Incoming;
using D.Core.Domain.Entities.File;
using D.Core.Domain.Entities.Hrm.DanhMuc;
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

            CreateMap<DmChucVu, DmChucVuResponseDto>();
            CreateMap<DmDanToc, DmDanTocResponseDto>();
            CreateMap<DmGioiTinh, DmGioiTinhResponseDto>();
            CreateMap<DmLoaiHopDong, DmLoaiHopDongResponseDto>();
            CreateMap<DmLoaiPhongBan, DmLoaiPhongBanResponseDto>();
            CreateMap<DmPhongBan, DmPhongBanResponseDto>();
            CreateMap<DmQuanHeGiaDinh, DmQuanHeGiaDinhResponseDto>();
            CreateMap<DmQuocTich, DmQuocTichResponseDto>();
            CreateMap<DmToBoMon, DmToBoMonResponseDto>();
            CreateMap<DmTonGiao, DmTonGiaoResponseDto>();
            CreateMap<CreateDmPhongBanDto, DmPhongBan>();
            CreateMap<CreateDmChucVuDto, DmChucVu>();
            CreateMap<CreateDmToBoMonDto, DmToBoMon>();
            CreateMap<CreateDmKhoaHocDto, DmKhoaHoc>();

            CreateMap<CreateHopDongDto, NsHopDong>();
            CreateMap<NsNhanSu, NsNhanSuResponseDto>()
                .ForMember(dest => dest.IdNhanSu, options => options.MapFrom(src => src.Id))
                .ForMember(
                    dest => dest.HoTen,
                    options =>
                        options.MapFrom(src =>
                            string.Join(
                                " ",
                                new[] { src.HoDem, src.Ten }.Where(x =>
                                    !string.IsNullOrWhiteSpace(x)
                                )
                            )
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

            CreateMap<NsNhanSu, NsNhanSuFindByIdResponseDto>()
                .ForMember(dest => dest.IdNhanSu, options => options.MapFrom(src => src.Id));

            #endregion

            #region sv

            CreateMap<SvSinhVien, SvSinhVienResponseDto>();

            #endregion

            #region file

            CreateMap<FileManagement, FileResponseDto>();
            CreateMap<CreateFileDto, FileManagement>()
                .ForMember(dest => dest.Link, opt => opt.Ignore());

            #endregion

            #region Delegation
            CreateMap<DelegationIncoming, PageDelegationIncomingResultDto>();
            CreateMap<CreateRequestDto, DelegationIncoming>();
            CreateMap<DelegationIncoming, CreateResponseDto>();
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
