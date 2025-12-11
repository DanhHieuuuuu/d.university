using AutoMapper;
using AutoMapper.QueryableExtensions;
using D.Core.Domain.Dtos.SinhVien;
using D.Core.Domain.Entities.SinhVien;
using D.Core.Infrastructure.Services.SinhVien.Abstracts;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace D.Core.Infrastructure.Services.SinhVien.Implements
{
    public class SvSinhVienService : ServiceBase, ISvSinhVienService
    {
        private readonly ServiceUnitOfWork _unitOfWork;

        public SvSinhVienService(
            ILogger<SvSinhVienService> logger,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork
        )
            : base(logger, httpContext, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public PageResultDto<SvSinhVienResponseDto> FindPagingSvSinhVien(SvSinhVienRequestDto dto)
        {
            _logger.LogInformation(
                $"{nameof(FindPagingSvSinhVien)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var query = _unitOfWork.iSvSinhVienRepository.TableNoTracking
            .Where(x => string.IsNullOrEmpty(dto.Keyword)
                     || x.Mssv.Contains(dto.Keyword)
                     || x.Ten.Contains(dto.Keyword));

            if (dto.Khoa.HasValue) query = query.Where(x => x.Khoa == dto.Khoa);
            if (dto.Nganh.HasValue) query = query.Where(x => x.Nganh == dto.Nganh);
            if (dto.KhoaHoc.HasValue) query = query.Where(x => x.KhoaHoc == dto.KhoaHoc);

            var items = query
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .ProjectTo<SvSinhVienResponseDto>(_mapper.ConfigurationProvider)
                .ToList();

            var totalCount = query.Count();

            return new PageResultDto<SvSinhVienResponseDto> { Items = items, TotalItem = totalCount };
        }

        public PageResultDto<SvSinhVienGetAllResponseDto> GetAllSinhVien(SvSinhVienGetAllRequestDto dto)
        {
            _logger.LogInformation(
                $"{nameof(GetAllSinhVien)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var query = _unitOfWork.iSvSinhVienRepository.TableNoTracking;

            if (dto.Khoa.HasValue) query = query.Where(x => x.Khoa == dto.Khoa);
            if (dto.Nganh.HasValue) query = query.Where(x => x.Nganh == dto.Nganh);
            if (dto.KhoaHoc.HasValue) query = query.Where(x => x.KhoaHoc == dto.KhoaHoc);

            var items = query
                .ProjectTo<SvSinhVienGetAllResponseDto>(_mapper.ConfigurationProvider)
                .ToList();

            return new PageResultDto<SvSinhVienGetAllResponseDto>
            {
                Items = items,
                TotalItem = query.Count()
            };
        }


        public async Task<SvSinhVienResponseDto> CreateSinhVien(CreateSinhVienDto dto)
        {
            _logger.LogInformation(
                $"{nameof(CreateSinhVien)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var exist = _unitOfWork.iSvSinhVienRepository.IsSoCccdExits(dto.SoCccd!);

            if (exist)
                throw new Exception("Sinh viên đã tồn tại trong hệ thống (trùng Số CCCD).");

            var newSv = _mapper.Map<SvSinhVien>(dto);

            newSv.Mssv = await _unitOfWork.iSvSinhVienRepository.GenerateMssv(dto.KhoaHoc!.Value);
            newSv.Email2 = _unitOfWork.iSvSinhVienRepository.GenerateEmail(newSv.Mssv!);
           
            _unitOfWork.iSvSinhVienRepository.AddAsync(newSv);
            _unitOfWork.iSvSinhVienRepository.SaveChangeAsync();

            return _mapper.Map<SvSinhVienResponseDto>(newSv);
        }

        public async Task<bool> UpdateSinhVien(UpdateSinhVienDto dto)
        {
            var sv = await _unitOfWork.iSvSinhVienRepository.GetByMssv(dto.Mssv!);
            if (sv == null) throw new Exception("Không tìm thấy sinh viên.");

            _mapper.Map(dto, sv);

            _unitOfWork.iSvSinhVienRepository.Update(sv);
            await _unitOfWork.iSvSinhVienRepository.SaveChangeAsync();
            return true;
        }

        public async Task<bool> DeleteSinhVien(DeleteSinhVienDto dto)
        {
            var sv = await _unitOfWork.iSvSinhVienRepository.GetByMssv(dto.Mssv!);
            if (sv == null) throw new Exception("Không tìm thấy sinh viên.");

            _unitOfWork.iSvSinhVienRepository.Delete(sv);
            await _unitOfWork.iSvSinhVienRepository.SaveChangeAsync();
            return true;
        }

        public async Task<SvSinhVienResponseDto> FindByMssv(FindByMssvDto dto)
        {
            _logger.LogInformation(
                $"{nameof(FindByMssv)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var result = await _unitOfWork.iSvSinhVienRepository.TableNoTracking
                .Where(x => x.Mssv == dto.Mssv) // Tìm chính xác theo MSSV
                .ProjectTo<SvSinhVienResponseDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (result == null)
                throw new Exception($"Không tìm thấy sinh viên có mssv: {dto.Mssv}");

            return result;
        }

    }
}
