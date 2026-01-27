using D.Core.Domain.Dtos.SinhVien;
using D.Core.Domain.Dtos.SinhVien.Auth;
using D.Core.Domain.Dtos.SinhVien.ThongTinChiTiet;
using D.DomainBase.Dto;

namespace D.Core.Infrastructure.Services.SinhVien.Abstracts
{
    public interface ISvSinhVienService
    {
        PageResultDto<SvSinhVienResponseDto> FindPagingSvSinhVien(SvSinhVienRequestDto dto);
        PageResultDto<SvSinhVienGetAllResponseDto> GetAllSinhVien(SvSinhVienGetAllRequestDto dto);
        Task<SvSinhVienResponseDto> CreateSinhVien(CreateSinhVienDto dto);
        Task<bool> UpdateSinhVien(UpdateSinhVienDto dto);
        Task<bool> DeleteSinhVien(DeleteSinhVienDto dto);
        Task<SvSinhVienResponseDto> FindByMssv(FindByMssvDto dto);
        Task<SvThongTinChiTietResponseDto> GetThongTinChiTiet(SvThongTinChiTietRequestDto dto);

        Task<SvLoginResponseDto> Login(SvLoginRequestDto loginRequest);
        Task<bool> Logout(SvLogoutRequestDto logoutRequestDto);
        Task<SvRefreshTokenResponseDto> RefreshToken(SvRefreshTokenRequestDto refreshToken);
    }
}
