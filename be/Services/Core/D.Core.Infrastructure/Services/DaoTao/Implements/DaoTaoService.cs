using AutoMapper;
using AutoMapper.QueryableExtensions;
using D.Core.Domain.Dtos.DaoTao.Khoa;
using D.Core.Domain.Entities.DaoTao;
using D.Core.Infrastructure.Services.DaoTao.Abstracts;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Services.DaoTao.Implements
{
    public class DaoTaoService : ServiceBase, IDaoTaoService
    {
        private readonly ServiceUnitOfWork _unitOfWork;

        public DaoTaoService(
            ILogger<DaoTaoService> logger,
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork
        )
            : base(logger, contextAccessor, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        #region Khoa
        public async Task<PageResultDto<DtKhoaResponseDto>> GetAllDtKhoa(DtKhoaRequestDto dto)
        {
            _logger.LogInformation($"{nameof(GetAllDtKhoa)} method called. Dto: {JsonSerializer.Serialize(dto)}");

            var query = _unitOfWork.iDtKhoaRepository.TableNoTracking
                .Where(x => string.IsNullOrEmpty(dto.Keyword)
                         || x.TenKhoa.Contains(dto.Keyword)
                         || x.MaKhoa.Contains(dto.Keyword));

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(x => x.Id)
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .ProjectTo<DtKhoaResponseDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PageResultDto<DtKhoaResponseDto>
            {
                Items = items,
                TotalItem = totalCount
            };
        }

        public async Task CreateDtKhoa(CreateDtKhoaDto dto)
        {
            _logger.LogInformation($"{nameof(CreateDtKhoa)} method called. Dto: {JsonSerializer.Serialize(dto)}");

            if (await _unitOfWork.iDtKhoaRepository.IsMaKhoaExistAsync(dto.MaKhoa!))
            {
                throw new Exception($"Đã tồn tại khoa có mã {dto.MaKhoa}");
            }

            var newKhoa = _mapper.Map<DtKhoa>(dto);

            await _unitOfWork.iDtKhoaRepository.AddAsync(newKhoa);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateDtKhoa(UpdateDtKhoaDto dto)
        {
            _logger.LogInformation($"{nameof(UpdateDtKhoa)} method called. Dto: {JsonSerializer.Serialize(dto)}");

            var existKhoa = await _unitOfWork.iDtKhoaRepository.TableNoTracking
                                     .FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (existKhoa == null)
            {
                throw new Exception("Không tìm thấy khoa này.");
            }

            bool existMaKhoa = await _unitOfWork.iDtKhoaRepository.TableNoTracking
                .AnyAsync(x => x.MaKhoa == dto.MaKhoa && x.Id != dto.Id);

            if (existMaKhoa)
            {
                throw new Exception($"Đã tồn tại mã khoa \"{dto.MaKhoa}\" trong hệ thống.");
            }

            _mapper.Map(dto, existKhoa);

            _unitOfWork.iDtKhoaRepository.Update(existKhoa);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteDtKhoa(int id)
        {
            _logger.LogInformation($"{nameof(DeleteDtKhoa)} called. Id: {id}");

            var existKhoa = await _unitOfWork.iDtKhoaRepository.TableNoTracking
                                     .FirstOrDefaultAsync(x => x.Id == id);

            if (existKhoa == null)
            {
                throw new Exception("Khoa không tồn tại hoặc đã bị xóa.");
            }

            _unitOfWork.iDtKhoaRepository.Delete(existKhoa);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<DtKhoaResponseDto> GetDtKhoaById(int id)
        {
            var entity = await _unitOfWork.iDtKhoaRepository.TableNoTracking
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null) throw new Exception("Không tìm thấy dữ liệu.");

            return _mapper.Map<DtKhoaResponseDto>(entity);
        }
        #endregion
    }
}
