using D.Core.Domain.Dtos.SinhVien;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Services.SinhVien.Abstracts
{
    public interface ISvSinhVienService
    {
        PageResultDto<SvSinhVienResponseDto> FindPagingSvSinhVien(SvSinhVienRequestDto dto);
        Task<int> CreateAsync(SvSinhVienCreateRequestDto dto);
        Task<bool> UpdateAsync(SvSinhVienUpdateRequestDto dto);
        Task<bool> DeleteAsync(int id);
        Task<SvSinhVienResponseDto> GetByIdAsync(int id);

    }
}
