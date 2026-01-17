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
using D.Core.Domain.Dtos.Kpi.KpiCaNhan;
using D.Core.Domain.Dtos.Kpi.KpiDonVi;
using D.Core.Domain.Dtos.Kpi.KpiRole;
using D.Core.Domain.Dtos.Kpi.KpiTruong;
using D.Core.Domain.Dtos.Hrm.QuanHeGiaDinh;
using D.Core.Domain.Dtos.SinhVien;
using D.Core.Domain.Dtos.Survey.Request;
using D.Core.Domain.Dtos.Survey.Submit;
using D.Core.Domain.Dtos.Survey.Surveys;
using D.Core.Domain.Entities.DaoTao;
using D.Core.Domain.Entities.Delegation.Incoming;
using D.Core.Domain.Entities.File;
using D.Core.Domain.Entities.Hrm.DanhMuc;
using D.Core.Domain.Entities.Hrm.NhanSu;
using D.Core.Domain.Entities.Kpi;
using D.Core.Domain.Entities.Kpi.Constants;
using D.Core.Domain.Entities.SinhVien;
using D.Core.Domain.Entities.Survey;
using D.Core.Domain.Entities.Survey.Constants;
using System.Reflection;
using D.Core.Domain.Dtos.Hrm.HopDong;
using D.Core.Domain.Dtos.Hrm.QuyetDinh;
using D.Core.Domain.Dtos.Kpi.KpiLogStatus;
using D.Core.Domain.Dtos.Kpi.KpiCongThuc;

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
            CreateMap<NsNhanSu, NsNhanSuHoSoChiTietResponseDto>()
                .ForMember(dest => dest.IdNhanSu, options => options.MapFrom(src => src.Id))
                .ForMember(
                    dest => dest.HoTen,
                    opt =>
                        opt.MapFrom(src =>
                            string.Join(
                                " ",
                                new[] { src.HoDem, src.Ten }.Where(x =>
                                    !string.IsNullOrWhiteSpace(x)
                                )
                            )
                        )
                );

            CreateMap<CreateHopDongDto, NsHopDong>();
            CreateMap<NsHopDong, NsHopDongResponseDto>();

            CreateMap<NsQuyetDinh, NsQuyetDinhResponseDto>();

            #endregion

            #region sv

            CreateMap<SvSinhVien, SvSinhVienResponseDto>()
                .ForMember(dest => dest.IdSinhVien, opt => opt.MapFrom(src => src.Id));
            CreateMap<CreateSinhVienDto, SvSinhVien>()
                .ForMember(dest => dest.HoDem, opt => opt.MapFrom(src => src.HoDem != null ? src.HoDem.Trim() : null))
                .ForMember(dest => dest.Ten, opt => opt.MapFrom(src => src.Ten != null ? src.Ten.Trim() : null));
            CreateMap<UpdateSinhVienDto, SvSinhVien>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Mssv, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());
            CreateMap<SvSinhVien, SvSinhVienGetAllResponseDto>()
                .ForMember(dest => dest.IdSinhVien, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.HoTen, opt => opt.MapFrom(src => $"{src.HoDem} {src.Ten}".Trim()));

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
            CreateMap<CreatePrepareRequestItemDto, Prepare>();
            CreateMap<Prepare, CreatePrepareResponseDto>();
            CreateMap<Prepare, UpdatePrepareResponseDto>();


            #endregion

            #region Survey
            CreateMap<KsSurveyTarget, RequestSurveyTargetDto>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<KsSurveyCriteria, RequestSurveyCriteriaDto>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<KsSurveyQuestion, RequestSurveyQuestionDto>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<KsQuestionAnswer, RequestQuestionAnswerDto>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

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

            CreateMap<KsSurvey, SurveyResponseDto>();
            CreateMap<KsSurveyQuestion, SurveyExamDto>();
            CreateMap<KsQuestionAnswer, AnswerExamDto>();          
            CreateMap<KsSurveySubmissionAnswer, SavedAnswerDto>()
                .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(src => src.IdCauHoi))
                .ForMember(dest => dest.SelectedAnswerId, opt => opt.MapFrom(src => src.IdDapAnChon))
                .ForMember(dest => dest.TextResponse, opt => opt.MapFrom(src => src.CauTraLoiText));
            #endregion

            #region Kpi
            CreateMap<KpiRole, KpiRoleResponseDto>();
            CreateMap<CreateKpiRoleDto, KpiRole>();
            CreateMap<CreateKpiCaNhanDto, KpiCaNhan>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => KpiStatus.Assigned));
            CreateMap<KpiDonVi, KpiDonViDto>();
            CreateMap<CreateKpiDonViDto, KpiDonVi>()
            .ForMember(dest => dest.TrangThai, opt => opt.MapFrom(src => KpiStatus.Create));
            CreateMap<KpiTruong, KpiTruongDto>();
            CreateMap<CreateKpiTruongDto, KpiTruong>()
            .ForMember(dest => dest.TrangThai, opt => opt.MapFrom(src => KpiStatus.Create));
            CreateMap<KpiLogStatus, KpiLogStatusDto>();
            CreateMap<KpiCongThuc, KpiCongThucDto>();
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
