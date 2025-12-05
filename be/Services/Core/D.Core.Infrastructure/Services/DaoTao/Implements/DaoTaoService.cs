using AutoMapper;
using AutoMapper.QueryableExtensions;
using D.Core.Domain.Dtos.DaoTao.ChuongTrinhKhung;
using D.Core.Domain.Dtos.DaoTao.ChuongTrinhKhungMon;
using D.Core.Domain.Dtos.DaoTao.ChuyenNganh;
using D.Core.Domain.Dtos.DaoTao.Khoa;
using D.Core.Domain.Dtos.DaoTao.MonHoc;
using D.Core.Domain.Dtos.DaoTao.MonTienQuyet;
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

        #region ChuyenNganh
        public async Task<PageResultDto<DtChuyenNganhResponseDto>> GetAllDtChuyenNganh(DtChuyenNganhRequestDto dto)
        {
            _logger.LogInformation($"{nameof(GetAllDtChuyenNganh)} method called. Dto: {JsonSerializer.Serialize(dto)}");

            var query = _unitOfWork.iDtChuyenNganhRepository.TableNoTracking
                .Where(x => string.IsNullOrEmpty(dto.Keyword)
                         || x.TenChuyenNganh.Contains(dto.Keyword)
                         || x.MaChuyenNganh.Contains(dto.Keyword));

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(x => x.Id)
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .ProjectTo<DtChuyenNganhResponseDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PageResultDto<DtChuyenNganhResponseDto>
            {
                Items = items,
                TotalItem = totalCount
            };
        }

        public async Task CreateDtChuyenNganh(CreateDtChuyenNganhDto dto)
        {
            _logger.LogInformation($"{nameof(CreateDtChuyenNganh)} method called. Dto: {JsonSerializer.Serialize(dto)}");

            if (await _unitOfWork.iDtChuyenNganhRepository.IsMaChuyenNganhExistAsync(dto.MaChuyenNganh!))
            {
                throw new Exception($"Đã tồn tại môn học có mã {dto.MaChuyenNganh}");
            }

            var newChuyenNganh = _mapper.Map<DtChuyenNganh>(dto);

            await _unitOfWork.iDtChuyenNganhRepository.AddAsync(newChuyenNganh);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateDtChuyenNganh(UpdateDtChuyenNganhDto dto)
        {
            _logger.LogInformation($"{nameof(UpdateDtChuyenNganh)} method called. Dto: {JsonSerializer.Serialize(dto)}");

            var existChuyenNganh = await _unitOfWork.iDtChuyenNganhRepository.TableNoTracking
                                     .FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (existChuyenNganh == null)
            {
                throw new Exception("Không tìm thấy môn học này.");
            }

            bool existMaChuyenNganh = await _unitOfWork.iDtChuyenNganhRepository.TableNoTracking
                .AnyAsync(x => x.MaChuyenNganh == dto.MaChuyenNganh && x.Id != dto.Id);

            if (existMaChuyenNganh)
            {
                throw new Exception($"Đã tồn tại mã môn học \"{dto.MaChuyenNganh}\" trong hệ thống.");
            }

            _mapper.Map(dto, existChuyenNganh);

            _unitOfWork.iDtChuyenNganhRepository.Update(existChuyenNganh);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteDtChuyenNganh(int id)
        {
            _logger.LogInformation($"{nameof(DeleteDtChuyenNganh)} called. Id: {id}");

            var existChuyenNganh = await _unitOfWork.iDtChuyenNganhRepository.TableNoTracking
                                     .FirstOrDefaultAsync(x => x.Id == id);

            if (existChuyenNganh == null)
            {
                throw new Exception("Môn học không tồn tại hoặc đã bị xóa.");
            }

            _unitOfWork.iDtChuyenNganhRepository.Delete(existChuyenNganh);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<DtChuyenNganhResponseDto> GetDtChuyenNganhById(int id)
        {
            var entity = await _unitOfWork.iDtChuyenNganhRepository.TableNoTracking
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null) throw new Exception("Không tìm thấy dữ liệu.");

            return _mapper.Map<DtChuyenNganhResponseDto>(entity);
        }
        #endregion

        #region MonHoc
        public async Task<PageResultDto<DtMonHocResponseDto>> GetAllDtMonHoc(DtMonHocRequestDto dto)
        {
            _logger.LogInformation($"{nameof(GetAllDtMonHoc)} method called. Dto: {JsonSerializer.Serialize(dto)}");

            var query = _unitOfWork.iDtMonHocRepository.TableNoTracking
                .Where(x => string.IsNullOrEmpty(dto.Keyword)
                         || x.TenMonHoc.Contains(dto.Keyword)
                         || x.MaMonHoc.Contains(dto.Keyword));

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(x => x.Id)
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .ProjectTo<DtMonHocResponseDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PageResultDto<DtMonHocResponseDto>
            {
                Items = items,
                TotalItem = totalCount
            };
        }

        public async Task CreateDtMonHoc(CreateDtMonHocDto dto)
        {
            _logger.LogInformation($"{nameof(CreateDtMonHoc)} method called. Dto: {JsonSerializer.Serialize(dto)}");

            if (await _unitOfWork.iDtMonHocRepository.IsMaMonHocExistAsync(dto.MaMonHoc!))
            {
                throw new Exception($"Đã tồn tại môn học có mã {dto.MaMonHoc}");
            }

            var newMonHoc = _mapper.Map<DtMonHoc>(dto);

            await _unitOfWork.iDtMonHocRepository.AddAsync(newMonHoc);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateDtMonHoc(UpdateDtMonHocDto dto)
        {
            _logger.LogInformation($"{nameof(UpdateDtMonHoc)} method called. Dto: {JsonSerializer.Serialize(dto)}");

            var existMonHoc = await _unitOfWork.iDtMonHocRepository.TableNoTracking
                                     .FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (existMonHoc == null)
            {
                throw new Exception("Không tìm thấy môn học này.");
            }

            bool existMaMonHoc = await _unitOfWork.iDtMonHocRepository.TableNoTracking
                .AnyAsync(x => x.MaMonHoc == dto.MaMonHoc && x.Id != dto.Id);

            if (existMaMonHoc)
            {
                throw new Exception($"Đã tồn tại mã môn học \"{dto.MaMonHoc}\" trong hệ thống.");
            }

            _mapper.Map(dto, existMonHoc);

            _unitOfWork.iDtMonHocRepository.Update(existMonHoc);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteDtMonHoc(int id)
        {
            _logger.LogInformation($"{nameof(DeleteDtMonHoc)} called. Id: {id}");

            var existMonHoc = await _unitOfWork.iDtMonHocRepository.TableNoTracking
                                     .FirstOrDefaultAsync(x => x.Id == id);

            if (existMonHoc == null)
            {
                throw new Exception("Môn học không tồn tại hoặc đã bị xóa.");
            }

            _unitOfWork.iDtMonHocRepository.Delete(existMonHoc);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<DtMonHocResponseDto> GetDtMonHocById(int id)
        {
            var entity = await _unitOfWork.iDtMonHocRepository.TableNoTracking
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null) throw new Exception("Không tìm thấy dữ liệu.");

            return _mapper.Map<DtMonHocResponseDto>(entity);
        }
        #endregion

        #region MonTienQuyet
        public async Task<PageResultDto<DtMonTienQuyetResponseDto>> GetAllDtMonTienQuyet(DtMonTienQuyetRequestDto dto)
        {
            _logger.LogInformation($"{nameof(GetAllDtMonTienQuyet)} method called. Dto: {JsonSerializer.Serialize(dto)}");

            var query = _unitOfWork.iDtMonTienQuyetRepository.TableNoTracking
                .Include(x => x.MonHoc)
                .Include(x => x.MonTienQuyet)
                .Where(x => string.IsNullOrEmpty(dto.Keyword)
                         // Mã/Tên môn chính
                         || x.MonHoc.MaMonHoc.Contains(dto.Keyword) 
                         || x.MonHoc.TenMonHoc.Contains(dto.Keyword)
                         // Mã/Tên môn tiên quyết
                         || x.MonTienQuyet.MaMonHoc.Contains(dto.Keyword)
                         || x.MonTienQuyet.TenMonHoc.Contains(dto.Keyword));

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(x => x.Id)
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .ProjectTo<DtMonTienQuyetResponseDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PageResultDto<DtMonTienQuyetResponseDto>
            {
                Items = items,
                TotalItem = totalCount
            };
        }

        public async Task CreateDtMonTienQuyet(CreateDtMonTienQuyetDto dto)
        {
            _logger.LogInformation($"{nameof(CreateDtMonTienQuyet)} method called. Dto: {JsonSerializer.Serialize(dto)}");

            if (dto.MonHocId == dto.MonTienQuyetId)
            {
                throw new Exception("Môn học và môn tiên quyết không được trùng nhau.");
            }

            if (await _unitOfWork.iDtMonTienQuyetRepository.IsRelationshipExistAsync(dto.MonHocId, dto.MonTienQuyetId))
            {
                throw new Exception("Môn học này đã có điều kiện môn tiên quyết này rồi.");
            }

            bool isReverseExist = await _unitOfWork.iDtMonTienQuyetRepository.TableNoTracking
                .AnyAsync(x => x.MonHocId == dto.MonTienQuyetId && x.MonTienQuyetId == dto.MonHocId);

            if (isReverseExist)
            {
                throw new Exception("Môn này đã là điều kiện của môn kia, không thể thiết lập ngược lại.");
            }

            var newEntity = _mapper.Map<DtMonTienQuyet>(dto);

            await _unitOfWork.iDtMonTienQuyetRepository.AddAsync(newEntity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateDtMonTienQuyet(UpdateDtMonTienQuyetDto dto)
        {
            _logger.LogInformation($"{nameof(UpdateDtMonTienQuyet)} method called. Dto: {JsonSerializer.Serialize(dto)}");

            var existMonTienQuyet = await _unitOfWork.iDtMonTienQuyetRepository.TableNoTracking
                                     .FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (existMonTienQuyet == null)
            {
                throw new Exception("Không tìm thấy môn tiên quyết này.");
            }

            existMonTienQuyet.LoaiDieuKien = dto.LoaiDieuKien;
            existMonTienQuyet.GhiChu = dto.GhiChu;

            _unitOfWork.iDtMonTienQuyetRepository.Update(existMonTienQuyet);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteDtMonTienQuyet(int id)
        {
            _logger.LogInformation($"{nameof(DeleteDtMonTienQuyet)} called. Id: {id}");

            var existMonTienQuyet = await _unitOfWork.iDtMonTienQuyetRepository.TableNoTracking
                                     .FirstOrDefaultAsync(x => x.Id == id);

            if (existMonTienQuyet == null)
            {
                throw new Exception("Môn tiên quyết không tồn tại hoặc đã bị xóa.");
            }

            _unitOfWork.iDtMonTienQuyetRepository.Delete(existMonTienQuyet);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<DtMonTienQuyetResponseDto> GetDtMonTienQuyetById(int id)
        {
            var entity = await _unitOfWork.iDtMonTienQuyetRepository.TableNoTracking
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null) throw new Exception("Không tìm thấy dữ liệu.");

            return _mapper.Map<DtMonTienQuyetResponseDto>(entity);
        }
        #endregion

        #region ChuongTrinhKhung
        public async Task<PageResultDto<DtChuongTrinhKhungResponseDto>> GetAllDtChuongTrinhKhung(DtChuongTrinhKhungRequestDto dto)
        {
            _logger.LogInformation($"{nameof(GetAllDtChuongTrinhKhung)} method called. Dto: {JsonSerializer.Serialize(dto)}");

            var query = _unitOfWork.iDtChuongTrinhKhungRepository.TableNoTracking
                .Where(x => string.IsNullOrEmpty(dto.Keyword)
                         || x.TenChuongTrinhKhung.Contains(dto.Keyword)
                         || x.MaChuongTrinhKhung.Contains(dto.Keyword));

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(x => x.Id)
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .ProjectTo<DtChuongTrinhKhungResponseDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PageResultDto<DtChuongTrinhKhungResponseDto>
            {
                Items = items,
                TotalItem = totalCount
            };
        }

        public async Task CreateDtChuongTrinhKhung(CreateDtChuongTrinhKhungDto dto)
        {
            _logger.LogInformation($"{nameof(CreateDtChuongTrinhKhung)} method called. Dto: {JsonSerializer.Serialize(dto)}");

            if (await _unitOfWork.iDtChuongTrinhKhungRepository.IsMaChuongTrinhKhungExistAsync(dto.MaChuongTrinhKhung!))
            {
                throw new Exception($"Đã tồn tại chương trình khung có mã {dto.MaChuongTrinhKhung}");
            }

            var newChuongTrinhKhung = _mapper.Map<DtChuongTrinhKhung>(dto);

            await _unitOfWork.iDtChuongTrinhKhungRepository.AddAsync(newChuongTrinhKhung);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateDtChuongTrinhKhung(UpdateDtChuongTrinhKhungDto dto)
        {
            _logger.LogInformation($"{nameof(UpdateDtChuongTrinhKhung)} method called. Dto: {JsonSerializer.Serialize(dto)}");

            var existChuongTrinhKhung = await _unitOfWork.iDtChuongTrinhKhungRepository.TableNoTracking
                                     .FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (existChuongTrinhKhung == null)
            {
                throw new Exception("Không tìm thấy chương trình khung này.");
            }

            bool existMaChuongTrinhKhung = await _unitOfWork.iDtChuongTrinhKhungRepository.TableNoTracking
                .AnyAsync(x => x.MaChuongTrinhKhung == dto.MaChuongTrinhKhung && x.Id != dto.Id);

            if (existMaChuongTrinhKhung)
            {
                throw new Exception($"Đã tồn tại mã chương trình khung \"{dto.MaChuongTrinhKhung}\" trong hệ thống.");
            }

            _mapper.Map(dto, existChuongTrinhKhung);

            _unitOfWork.iDtChuongTrinhKhungRepository.Update(existChuongTrinhKhung);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteDtChuongTrinhKhung(int id)
        {
            _logger.LogInformation($"{nameof(DeleteDtChuongTrinhKhung)} called. Id: {id}");

            var existChuongTrinhKhung = await _unitOfWork.iDtChuongTrinhKhungRepository.TableNoTracking
                                     .FirstOrDefaultAsync(x => x.Id == id);

            if (existChuongTrinhKhung == null)
            {
                throw new Exception("Môn học không tồn tại hoặc đã bị xóa.");
            }

            _unitOfWork.iDtChuongTrinhKhungRepository.Delete(existChuongTrinhKhung);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<DtChuongTrinhKhungResponseDto> GetDtChuongTrinhKhungById(int id)
        {
            var entity = await _unitOfWork.iDtChuongTrinhKhungRepository.TableNoTracking
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null) throw new Exception("Không tìm thấy dữ liệu.");

            return _mapper.Map<DtChuongTrinhKhungResponseDto>(entity);
        }
        #endregion

        #region ChuongTrinhKhungMon
        public async Task<PageResultDto<DtChuongTrinhKhungMonResponseDto>> GetAllDtChuongTrinhKhungMon(DtChuongTrinhKhungMonRequestDto dto)
        {
            _logger.LogInformation($"{nameof(GetAllDtChuongTrinhKhungMon)} method called. Dto: {JsonSerializer.Serialize(dto)}");

            var query = _unitOfWork.iDtChuongTrinhKhungMonRepository.TableNoTracking
                .Include(x => x.MonHoc)
                .Where(x => true);

            if (dto.ChuongTrinhKhungId.HasValue)
            {
                query = query.Where(x => x.ChuongTrinhKhungId == dto.ChuongTrinhKhungId);
            }

            if (!string.IsNullOrEmpty(dto.Keyword))
            {
                query = query.Where(x => x.MonHoc.MaMonHoc.Contains(dto.Keyword)
                                      || x.MonHoc.TenMonHoc.Contains(dto.Keyword));
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(x => x.HocKy)
                .ThenBy(x => x.MonHoc.MaMonHoc)
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .ProjectTo<DtChuongTrinhKhungMonResponseDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PageResultDto<DtChuongTrinhKhungMonResponseDto>
            {
                Items = items,
                TotalItem = totalCount
            };
        }

        public async Task CreateDtChuongTrinhKhungMon(CreateDtChuongTrinhKhungMonDto dto)
        {
            _logger.LogInformation($"{nameof(CreateDtChuongTrinhKhungMon)} method called. Dto: {JsonSerializer.Serialize(dto)}");

            if (await _unitOfWork.iDtChuongTrinhKhungMonRepository.IsExistAsync(dto.ChuongTrinhKhungId, dto.MonHocId))
            {
                throw new Exception("Môn học này đã có trong chương trình khung.");
            }

            var newChuongTrinhKhungMon = _mapper.Map<DtChuongTrinhKhungMon>(dto);

            await _unitOfWork.iDtChuongTrinhKhungMonRepository.AddAsync(newChuongTrinhKhungMon);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateDtChuongTrinhKhungMon(UpdateDtChuongTrinhKhungMonDto dto)
        {
            _logger.LogInformation($"{nameof(UpdateDtChuongTrinhKhungMon)} method called. Dto: {JsonSerializer.Serialize(dto)}");

            var existChuongTrinhKhungMon = await _unitOfWork.iDtChuongTrinhKhungMonRepository.TableNoTracking
                                     .FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (existChuongTrinhKhungMon == null)
            {
                throw new Exception("Không tìm thấy chương trình khung môn này.");
            }

            _mapper.Map(dto, existChuongTrinhKhungMon);

            _unitOfWork.iDtChuongTrinhKhungMonRepository.Update(existChuongTrinhKhungMon);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteDtChuongTrinhKhungMon(int id)
        {
            _logger.LogInformation($"{nameof(DeleteDtChuongTrinhKhungMon)} called. Id: {id}");

            var existChuongTrinhKhungMon = await _unitOfWork.iDtChuongTrinhKhungMonRepository.TableNoTracking
                                     .FirstOrDefaultAsync(x => x.Id == id);

            if (existChuongTrinhKhungMon == null)
            {
                throw new Exception("Môn học không tồn tại hoặc đã bị xóa.");
            }

            _unitOfWork.iDtChuongTrinhKhungMonRepository.Delete(existChuongTrinhKhungMon);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<DtChuongTrinhKhungMonResponseDto> GetDtChuongTrinhKhungMonById(int id)
        {
            var entity = await _unitOfWork.iDtChuongTrinhKhungMonRepository.TableNoTracking
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null) throw new Exception("Không tìm thấy dữ liệu.");

            return _mapper.Map<DtChuongTrinhKhungMonResponseDto>(entity);
        }
        #endregion
    }
}