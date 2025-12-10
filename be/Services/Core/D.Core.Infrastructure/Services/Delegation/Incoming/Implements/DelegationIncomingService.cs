using AutoMapper;
using D.Constants.Core.Delegation;
using D.Core.Domain;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming.Paging;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmChucVu;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmPhongBan;
using D.Core.Domain.Entities.Delegation.Incoming;
using D.Core.Domain.Entities.Hrm.DanhMuc;
using D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts;
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

namespace D.Core.Infrastructure.Services.Delegation.Incoming.Implements
{
    public class DelegationIncomingService : ServiceBase, IDelegationIncomingService
    {
        private readonly ServiceUnitOfWork _unitOfWork;

        public DelegationIncomingService(
            ILogger<DelegationIncomingService> logger,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork
        )
            : base(logger, httpContext, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Hàm paging
        /// </summary>
        /// <param name="dto">các biến đầu vào paging</param>
        /// <returns>PageResultDto</returns>
        public PageResultDto<PageDelegationIncomingResultDto> Paging(FilterDelegationIncomingDto dto)
        {
            _logger.LogInformation($"{nameof(Paging)} method called, dto: {JsonSerializer.Serialize(dto)}.");

            var query = _unitOfWork.iDelegationIncomingRepository.TableNoTracking.Where(x =>
                (string.IsNullOrEmpty(dto.Keyword)
                || x.Name.ToLower().Contains(dto.Keyword.ToLower()))
                && (dto.IdPhongBan == 0 || x.IdPhongBan == dto.IdPhongBan)
                && (dto.Status == 0 || x.Status == dto.Status)

            );

            var totalCount = query.Count();

            var items = query.Skip(dto.SkipCount()).Take(dto.PageSize).ToList();
            ;

            return new PageResultDto<PageDelegationIncomingResultDto>
            {
                Items = _mapper.Map<List<PageDelegationIncomingResultDto>>(items),
                TotalItem = totalCount,
            };
        }
        
        public async Task<CreateResponseDto> Create(CreateRequestDto dto)
        {
            _logger.LogInformation($"{nameof(Create)} method called, dto: {JsonSerializer.Serialize(dto)}.");

            bool isExist = _unitOfWork.iDelegationIncomingRepository.IsMaDoanVaoExist(dto.Code);
            if (isExist)
            {
                throw new Exception($"Đã tồn tại Đoàn vào có mã {dto.Code}");
            }
            var newDoanVao = _mapper.Map<DelegationIncoming>(dto);
            newDoanVao.Status = DelegationStatus.Create;
            _unitOfWork.iDelegationIncomingRepository.Add(newDoanVao);
            await _unitOfWork.SaveChangesAsync();           
            return _mapper.Map<CreateResponseDto>(newDoanVao);

        }

        public async Task<UpdateDelegationIncomingResponseDto> UpdateDelegationIncoming(UpdateDelegationIncomingRequestDto dto)
        {
            _logger.LogInformation(
                $"{nameof(UpdateDelegationIncoming)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var exist = _unitOfWork.iDelegationIncomingRepository.Table
                .FirstOrDefault(x => x.Id == dto.Id);

            if (exist == null)
                throw new Exception($"Không tìm thấy Đoàn vào này.");
                
            var existMaDoanVao = _unitOfWork.iDelegationIncomingRepository.Table
                .Any(x => x.Code == dto.Code && x.Id != dto.Id);

            if (existMaDoanVao)
                throw new Exception($"Đã tồn tại mã Đoàn vào \"{dto.Code}\".");

            // Cập nhật 
            exist.Code = dto.Code;
            exist.Name = dto.Name;
            exist.Content = dto.Content;
            exist.IdPhongBan = dto.IdPhongBan;
            exist.Location = dto.Location;
            exist.IdStaffReception = dto.IdStaffReception;
            exist.TotalPerson = dto.TotalPerson;
            exist.PhoneNumber = dto.PhoneNumber;
            exist.RequestDate = dto.RequestDate;
            exist.ReceptionDate = dto.ReceptionDate;
            exist.TotalMoney = dto.TotalMoney;
            exist.Status = DelegationStatus.Edited;

            _unitOfWork.iDelegationIncomingRepository.Update(exist);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<UpdateDelegationIncomingResponseDto>(exist);
        }

        public void DeleteDoanVao(int id)
        {
            _logger.LogInformation($"{nameof(DeleteDoanVao)} method called. Dto: {id}");

            var exist = _unitOfWork.iDelegationIncomingRepository.FindById(id);

            if (exist != null)
            {
                _unitOfWork.iDelegationIncomingRepository.Delete(exist);
                _unitOfWork.iDelegationIncomingRepository.SaveChange();
            }
            else
            {
                throw new Exception($"Đoàn vào không tồn tại hoặc đã bị xóa");
            }
        }


        public List<ViewPhongBanResponseDto> GetAllPhongBan(ViewPhongBanRequestDto dto)
        {
            _logger.LogInformation($"{nameof(GetAllPhongBan)} called.");

            var list = _unitOfWork.iDmPhongBanRepository.TableNoTracking
                .Where(x => x.IsActive == true)
                .OrderBy(x => x.STT ?? int.MaxValue)
                .Select(x => new ViewPhongBanResponseDto
                {
                    IdPhongBan = x.Id,
                    TenPhongBan = x.TenPhongBan
                })
                .ToList();

            return list;
        }
        public List<ViewTrangThaiResponseDto> GetListTrangThai(ViewTrangThaiRequestDto dto)
        {
            _logger.LogInformation($"{nameof(GetListTrangThai)}");

            var trangThaiExist = _unitOfWork.iDelegationIncomingRepository
                .TableNoTracking
                .Where(x => x.Status != null)
                .Select(x => x.Status)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            return trangThaiExist
                .Select(x => new ViewTrangThaiResponseDto { Status = x })
                .ToList();
        }

    }
}
