using D.Core.Domain.Dtos.DaoTao.Khoa;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Services.DaoTao.Abstracts
{
    public interface IDaoTaoService
    {
        Task<PageResultDto<DtKhoaResponseDto>> GetAllDtKhoa(DtKhoaRequestDto dto);
        Task CreateDtKhoa(CreateDtKhoaDto dto);
        Task UpdateDtKhoa(UpdateDtKhoaDto dto);
        Task DeleteDtKhoa(int id);
        Task<DtKhoaResponseDto> GetDtKhoaById(int id);
    }
}
