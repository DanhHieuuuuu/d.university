using AutoMapper;
using AutoMapper.QueryableExtensions;
using D.Core.Domain.Dtos.SinhVien;
using D.Core.Domain.Entities.SinhVien;
using D.Core.Infrastructure.Services.SinhVien.Abstracts;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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

            var query = _unitOfWork.iSvSinhVienRepository.TableNoTracking.Where(x =>
                string.IsNullOrEmpty(dto.Mssv) || dto.Mssv == x.Mssv
            );

            var totalCount = query.Count();

            var items = query
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .ProjectTo<SvSinhVienResponseDto>(_mapper.ConfigurationProvider)
                .ToList();

            return new PageResultDto<SvSinhVienResponseDto> { Items = items, TotalItem = totalCount };
        }

        public async Task<int> CreateAsync(SvSinhVienCreateRequestDto dto)
        {
            var entity = new SvSinhVien
            {
                //Mssv = dto.Mssv,
                HoDem = dto.HoDem,
                Ten = dto.Ten,
                NgaySinh = dto.NgaySinh,
                NoiSinh = dto.NoiSinh,
                GioiTinh = dto.GioiTinh,
                QuocTich = dto.QuocTich,
                DanToc = dto.DanToc,
                SoCccd = dto.SoCccd,
                SoDienThoai = dto.SoDienThoai
            };

            _unitOfWork.iSvSinhVienRepository.Add(entity);
            await _unitOfWork.iSvSinhVienRepository.SaveChangeAsync();
            return entity.Id;
        }

        public async Task<bool> UpdateAsync(SvSinhVienUpdateRequestDto dto)
        {
            //var entity = await _unitOfWork.iSvSinhVienRepository.GetByIdAsync(dto.Id);
            var entity = _unitOfWork.iSvSinhVienRepository.FindById(id);
            if (entity == null) return false;
            //entity.Mssv = dto.Mssv;
            entity.HoDem = dto.HoDem;
            entity.Ten = dto.Ten;
            entity.NgaySinh = dto.NgaySinh;
            entity.NoiSinh = dto.NoiSinh;
            entity.GioiTinh = dto.GioiTinh;
            entity.QuocTich = dto.QuocTich;
            entity.DanToc = dto.DanToc;
            entity.SoCccd = dto.SoCccd;
            entity.SoDienThoai = dto.SoDienThoai;  

            _unitOfWork.iSvSinhVienRepository.Update(entity);
            await _unitOfWork.iSvSinhVienRepository.SaveChangeAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            //var entity = await _unitOfWork.iSvSinhVienRepository.GetByIdAsync(id);
            var entity = _unitOfWork.iSvSinhVienRepository.FindById(id);
            if (entity == null) return false;

            _unitOfWork.iSvSinhVienRepository.Delete(entity);
            await _unitOfWork.iSvSinhVienRepository.SaveChangeAsync();
            return true;
        }

        public async Task<SvSinhVienResponseDto> GetByIdAsync(int id)
        {
            //var entity = await _unitOfWork.iSvSinhVienRepository.GetByIdAsync(id);
            var entity = _unitOfWork.iSvSinhVienRepository.FindById(id);
            if (entity == null) return null;
            return _mapper.Map<SvSinhVienResponseDto>(entity);
        }
    }
}
