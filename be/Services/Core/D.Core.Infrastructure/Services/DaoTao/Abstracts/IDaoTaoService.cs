using D.Core.Domain.Dtos.DaoTao.ChuyenNganh;
using D.Core.Domain.Dtos.DaoTao.Khoa;
using D.Core.Domain.Dtos.DaoTao.MonHoc;
using D.Core.Domain.Dtos.DaoTao.Nganh;
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

        Task<PageResultDto<DtNganhResponseDto>> GetAllDtNganh(DtNganhRequestDto dto);
        Task CreateDtNganh(CreateDtNganhDto dto);
        Task UpdateDtNganh(UpdateDtNganhDto dto);
        Task DeleteDtNganh(int id);
        Task<DtNganhResponseDto> GetDtNganhById(int id);

        Task<PageResultDto<DtChuyenNganhResponseDto>> GetAllDtChuyenNganh(DtChuyenNganhRequestDto dto);
        Task CreateDtChuyenNganh(CreateDtChuyenNganhDto dto);
        Task UpdateDtChuyenNganh(UpdateDtChuyenNganhDto dto);
        Task DeleteDtChuyenNganh(int id);
        Task<DtChuyenNganhResponseDto> GetDtChuyenNganhById(int id);

        Task<PageResultDto<DtMonHocResponseDto>> GetAllDtMonHoc(DtMonHocRequestDto dto);
        Task CreateDtMonHoc(CreateDtMonHocDto dto);
        Task UpdateDtMonHoc(UpdateDtMonHocDto dto);
        Task DeleteDtMonHoc(int id);
        Task<DtMonHocResponseDto> GetDtMonHocById(int id);
    }
}
