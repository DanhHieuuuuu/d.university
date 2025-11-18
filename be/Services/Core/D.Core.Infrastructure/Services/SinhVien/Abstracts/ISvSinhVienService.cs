using D.Core.Domain.Dtos.SinhVien;
using D.DomainBase.Dto;

namespace D.Core.Infrastructure.Services.SinhVien.Abstracts
{
    public interface ISvSinhVienService
    {
        PageResultDto<SvSinhVienResponseDto> FindPagingSvSinhVien(SvSinhVienRequestDto dto);
        PageResultDto<SvSinhVienGetAllResponseDto> GetAllSinhVien(SvSinhVienGetAllRequestDto dto);
        SvSinhVienResponseDto CreateSinhVien(CreateSinhVienDto dto);
        Task<bool> UpdateSinhVien(UpdateSinhVienDto dto);
        Task<bool> DeleteSinhVien(DeleteSinhVienDto dto);
        SvSinhVienResponseDto FindByMssv(FindByMssvDto dto);
    }
}
