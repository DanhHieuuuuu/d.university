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
using D.DomainBase.Dto;

namespace D.Core.Infrastructure.Services.Hrm.Abstracts
{
    public interface IDmDanhMucService
    {
        public PageResultDto<DmChucVuResponseDto> GetAllDmChucVu(DmChucVuRequestDto dto);
        public PageResultDto<DmDanTocResponseDto> GetAllDmDanToc(DmDanTocRequestDto dto);
        public PageResultDto<DmGioiTinhResponseDto> GetAllDmGioiTinh(DmGioiTinhRequestDto dto);
        public PageResultDto<DmLoaiHopDongResponseDto> GetAllDmLoaiHopDong(DmLoaiHopDongRequestDto dto);
        public PageResultDto<DmLoaiPhongBanResponseDto> GetAllDmLoaiPhongBan(DmLoaiPhongBanRequestDto dto);
        public PageResultDto<DmPhongBanResponseDto> GetAllDmPhongBan(DmPhongBanRequestDto dto);
        public PageResultDto<DmPhongBanByKpiRoleResponseDto> GetAllDmPhongBanByKpiRole(DmPhongBanByKpiRoleRequestDto dto);
        public PageResultDto<DmKhoaHocResponseDto> GetAllDmKhoaHoc(DmKhoaHocRequestDto dto);
        public PageResultDto<DmQuanHeGiaDinhResponseDto> GetAllDmQuanHeGiaDinh(DmQuanHeGiaDinhRequestDto dto);
        public PageResultDto<DmQuocTichResponseDto> GetAllDmQuocTich(DmQuocTichRequestDto dto);
        public PageResultDto<DmToBoMonResponseDto> GetAllDmToBoMon(DmToBoMonRequestDto dto);
        public PageResultDto<DmTonGiaoResponseDto> GetAllDmTonGiao(DmTonGiaoRequestDto dto);
        public void CreateDmChucVu(CreateDmChucVuDto dto);
        public void UpdateDmChucVu(UpdateDmChucVuDto dto);
        public void DeleteDmChucVu(int id);
        public void CreateDmPhongBan(CreateDmPhongBanDto dto);
        public void UpdateDmPhongBan(UpdateDmPhongBanDto dto);
        public void DeleteDmPhongBan(int id);

        public void CreateDmKhoaHoc(CreateDmKhoaHocDto dto);
        #region To Bo Mon
        public void CreateDmToBoMon(CreateDmToBoMonDto dto);
        public void UpdateDmToBoMon(UpdateDmToBoMonDto dto);
        public void DeleteDmToBoMon(int id);
        public Task<DmToBoMonResponseDto> GetDmToBoMonByIdAsync(int id);

        #endregion

        public Task<DmChucVuResponseDto> GetDmChucVuByIdAsync(int id);
        public Task<DmPhongBanResponseDto> GetDmPhongBanByIdAsync(int id);
        public Task<DmKhoaHocResponseDto> GetDmKhoaHocByIdAsync(int id);
    }
}
