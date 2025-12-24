using System.Reflection;
using AutoMapper;
using D.Core.Domain.Dtos.DaoTao.ChuongTrinhKhung;
using D.Core.Domain.Dtos.DaoTao.ChuongTrinhKhungMon;
using D.Core.Domain.Dtos.DaoTao.ChuyenNganh;
using D.Core.Domain.Dtos.DaoTao.Khoa;
using D.Core.Domain.Dtos.DaoTao.MonHoc;
using D.Core.Domain.Dtos.DaoTao.MonTienQuyet;
using D.Core.Domain.Dtos.DaoTao.Nganh;
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
using D.Core.Domain.Dtos.Survey.Request;
using D.Core.Domain.Dtos.Survey.Surveys;
using D.Core.Domain.Entities.DaoTao;
using D.Core.Domain.Entities.Delegation.Incoming;
using D.Core.Domain.Entities.File;
using D.Core.Domain.Entities.Hrm.DanhMuc;
using D.Core.Domain.Entities.Hrm.NhanSu;
using D.Core.Domain.Entities.SinhVien;
using D.Core.Domain.Entities.Survey;
using D.Core.Domain.Entities.Survey.Constants;

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
            CreateMap<DmKhoaHoc, DmKhoaHocResponseDto>();

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
                .ForMember(
                    dest => dest.NhomMau,
                    opt =>
                        opt.MapFrom(src =>
                            !string.IsNullOrEmpty(src.NhomMau) ? src.NhomMau.ToUpper() : src.NhomMau
                        )
                )
                .ForMember(dst => dst.NgayCapNhatSk, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<NsNhanSu, NsNhanSuFindByIdResponseDto>()
                .ForMember(dest => dest.IdNhanSu, options => options.MapFrom(src => src.Id));

            #endregion

            #region sv

            CreateMap<SvSinhVien, SvSinhVienResponseDto>();
            CreateMap<CreateSinhVienDto, SvSinhVien>();
            CreateMap<UpdateSinhVienDto, SvSinhVien>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<SvSinhVien, SvSinhVienGetAllResponseDto>()
                .ForMember(
                    dest => dest.HoTen,
                    opt => opt.MapFrom(src => src.HoDem + " " + src.Ten)
                );

            #endregion

            #region DaoTao
            CreateMap<DtKhoa, DtKhoaResponseDto>();
            CreateMap<CreateDtKhoaDto, DtKhoa>();
            CreateMap<UpdateDtKhoaDto, DtKhoa>();

            CreateMap<DtNganh, DtNganhResponseDto>();
            CreateMap<CreateDtNganhDto, DtNganh>();
            CreateMap<UpdateDtNganhDto, DtNganh>();

            CreateMap<DtChuyenNganh, DtChuyenNganhResponseDto>();
            CreateMap<CreateDtChuyenNganhDto, DtChuyenNganh>();
            CreateMap<UpdateDtChuyenNganhDto, DtChuyenNganh>();

            CreateMap<DtChuongTrinhKhung, DtChuongTrinhKhungResponseDto>();
            CreateMap<CreateDtChuongTrinhKhungDto, DtChuongTrinhKhung>();
            CreateMap<UpdateDtChuongTrinhKhungDto, DtChuongTrinhKhung>();

            CreateMap<DtMonHoc, DtMonHocResponseDto>();
            CreateMap<CreateDtMonHocDto, DtMonHoc>();
            CreateMap<UpdateDtMonHocDto, DtMonHoc>();

            CreateMap<DtMonTienQuyet, DtMonTienQuyetResponseDto>();
            CreateMap<CreateDtMonTienQuyetDto, DtMonTienQuyet>();
            CreateMap<UpdateDtMonTienQuyetDto, DtMonTienQuyet>();

            CreateMap<DtChuongTrinhKhungMon, DtChuongTrinhKhungMonResponseDto>();
            CreateMap<CreateDtChuongTrinhKhungMonDto, DtChuongTrinhKhungMon>();
            CreateMap<UpdateDtChuongTrinhKhungMonDto, DtChuongTrinhKhungMon>();

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
            CreateMap<DelegationIncoming, UpdateDelegationIncomingResponseDto>();
            CreateMap<LogStatus, ViewDelegationIncomingLogDto>();
            CreateMap<LogReceptionTime, ViewReceptionTimeLogDto>();
            CreateMap<CreateReceptionTimeRequestDto, ReceptionTime>();
            CreateMap<ReceptionTime, CreateReceptionTimeResponseDto>();
            CreateMap<ReceptionTime, UpdateReceptionTimeResponseDto>();
            CreateMap<Supporter, CreateSupporterResponseDto>();
            CreateMap<CreateSupporterResponseDto, Supporter>();
            CreateMap<CreateSupporterRequestDto, Supporter>();
            CreateMap<CreateDepartmentSupportResponseDto, DepartmentSupport>();
            CreateMap<CreateDepartmentSupportRequestDto, DepartmentSupport>();
            CreateMap<DepartmentSupport, CreateDepartmentSupportResponseDto>();

            #endregion

            #region Survey
            CreateMap<KsSurveyTarget, RequestSurveyTargetDto>().ReverseMap();
            CreateMap<KsSurveyCriteria, RequestSurveyCriteriaDto>().ReverseMap();
            CreateMap<KsSurveyQuestion, RequestSurveyQuestionDto>().ReverseMap();
            CreateMap<KsQuestionAnswer, RequestQuestionAnswerDto>().ReverseMap();

            CreateMap<KsSurveyRequest, RequestSurveyResponseDto>();
            CreateMap<KsSurveyRequest, RequestSurveyDetailDto>();

            CreateMap<CreateRequestSurveyRequestDto, KsSurveyRequest>();
            CreateMap<KsSurveyRequest, CreateRequestSurveyResponseDto>();
            CreateMap<UpdateRequestSurveyRequestDto, KsSurveyRequest>();
            CreateMap<KsSurveyRequest, UpdateRequestSurveyResponseDto>();

            CreateMap<KsSurvey, SurveyResponseDto>();
            CreateMap<KsSurvey, SurveyDetailDto>();

            CreateMap<KsSurveyRequest, KsSurvey>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IdYeuCau, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.MaKhaoSat, opt => opt.MapFrom(src => src.MaYeuCau))
                .ForMember(dest => dest.TenKhaoSat, opt => opt.MapFrom(src => src.TenKhaoSatYeuCau));

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
