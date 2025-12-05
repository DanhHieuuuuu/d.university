using AutoMapper;
using AutoMapper.QueryableExtensions;
using D.Core.Domain.Dtos.DaoTao.Khoa;
using D.Core.Domain.Dtos.DaoTao.Nganh;
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

        #region Nganh
        public async Task<PageResultDto<DtNganhResponseDto>> GetAllDtNganh(DtNganhRequestDto dto)
        {
            _logger.LogInformation($"{nameof(GetAllDtNganh)} method called. Dto: {JsonSerializer.Serialize(dto)}");

            var query = _unitOfWork.iDtNganhRepository.TableNoTracking
                .Where(x => string.IsNullOrEmpty(dto.Keyword)
                         || x.TenNganh.Contains(dto.Keyword)
                         || x.MaNganh.Contains(dto.Keyword));

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(x => x.Id)
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .ProjectTo<DtNganhResponseDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PageResultDto<DtNganhResponseDto>
            {
                Items = items,
                TotalItem = totalCount
            };
        }

        public async Task CreateDtNganh(CreateDtNganhDto dto)
        {
            _logger.LogInformation($"{nameof(CreateDtNganh)} method called. Dto: {JsonSerializer.Serialize(dto)}");

            if (await _unitOfWork.iDtNganhRepository.IsMaNganhExistAsync(dto.MaNganh!))
            {
                throw new Exception($"Đã tồn tại ngành có mã {dto.MaNganh}");
            }

            var newNganh = _mapper.Map<DtNganh>(dto);

            await _unitOfWork.iDtNganhRepository.AddAsync(newNganh);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateDtNganh(UpdateDtNganhDto dto)
        {
            _logger.LogInformation($"{nameof(UpdateDtNganh)} method called. Dto: {JsonSerializer.Serialize(dto)}");

            var existNganh = await _unitOfWork.iDtNganhRepository.TableNoTracking
                                     .FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (existNganh == null)
            {
                throw new Exception("Không tìm thấy ngành này.");
            }

            bool existMaNganh = await _unitOfWork.iDtNganhRepository.TableNoTracking
                .AnyAsync(x => x.MaNganh == dto.MaNganh && x.Id != dto.Id);

            if (existMaNganh)
            {
                throw new Exception($"Đã tồn tại mã ngành \"{dto.MaNganh}\" trong hệ thống.");
            }

            _mapper.Map(dto, existNganh);

            _unitOfWork.iDtNganhRepository.Update(existNganh);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteDtNganh(int id)
        {
            _logger.LogInformation($"{nameof(DeleteDtNganh)} called. Id: {id}");

            var existNganh = await _unitOfWork.iDtNganhRepository.TableNoTracking
                                     .FirstOrDefaultAsync(x => x.Id == id);

            if (existNganh == null)
            {
                throw new Exception("Ngành không tồn tại hoặc đã bị xóa.");
            }

            _unitOfWork.iDtNganhRepository.Delete(existNganh);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<DtNganhResponseDto> GetDtNganhById(int id)
        {
            var entity = await _unitOfWork.iDtNganhRepository.TableNoTracking
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null) throw new Exception("Không tìm thấy dữ liệu.");

            return _mapper.Map<DtNganhResponseDto>(entity);
        }
        #endregion
    }
}
