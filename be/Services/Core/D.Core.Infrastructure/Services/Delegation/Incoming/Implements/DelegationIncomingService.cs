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
using Microsoft.AspNetCore.Http.HttpResults;
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

            var phongBanTable = _unitOfWork.iDmPhongBanRepository.TableNoTracking;
            var staffTable = _unitOfWork.iNsNhanSuRepository.TableNoTracking;
           
            var query = from d in _unitOfWork.iDelegationIncomingRepository.TableNoTracking
                        join pb in phongBanTable on d.IdPhongBan equals pb.Id into pbJoin
                        from pb in pbJoin.DefaultIfEmpty()
                        join s in staffTable on d.IdStaffReception equals s.Id into sJoin
                        from s in sJoin.DefaultIfEmpty()
                        where (string.IsNullOrEmpty(dto.Keyword) || d.Name.ToLower().Contains(dto.Keyword.ToLower()))
                              && (dto.IdPhongBan == 0 || d.IdPhongBan == dto.IdPhongBan)
                              && (dto.Status == 0 || d.Status == dto.Status)
                        select new PageDelegationIncomingResultDto
                        {
                            Id = d.Id,
                            Code = d.Code,
                            Name = d.Name,
                            Content = d.Content,
                            IdPhongBan = d.IdPhongBan,
                            PhongBan = pb != null ? pb.TenPhongBan : null,
                            IdStaffReception = d.IdStaffReception,
                            StaffReceptionName = s != null ? s.HoDem + " " + s.Ten : null,
                            TotalPerson = d.TotalPerson,
                            PhoneNumber = d.PhoneNumber,
                            Status = d.Status,
                            RequestDate = d.RequestDate,
                            ReceptionDate = d.ReceptionDate,
                            TotalMoney = d.TotalMoney
                        };

            var totalCount = query.Count();

            var items = query.Skip(dto.SkipCount()).Take(dto.PageSize).ToList();

            return new PageResultDto<PageDelegationIncomingResultDto>
            {
                Items = items,
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
        public async Task<PageDelegationIncomingResultDto> GetByIdDelegationIncoming(int id)
        {
            _logger.LogInformation($"{nameof(GetByIdDelegationIncoming)} called with id: {id}");

            var phongBanTable = _unitOfWork.iDmPhongBanRepository.TableNoTracking;
            var staffTable = _unitOfWork.iNsNhanSuRepository.TableNoTracking;

            var query = from d in _unitOfWork.iDelegationIncomingRepository.TableNoTracking
                        where d.Id == id
                        join pb in phongBanTable on d.IdPhongBan equals pb.Id into pbJoin
                        from pb in pbJoin.DefaultIfEmpty()
                        join s in staffTable on d.IdStaffReception equals s.Id into sJoin
                        from s in sJoin.DefaultIfEmpty()
                        select new PageDelegationIncomingResultDto
                        {
                            Id = d.Id,
                            Code = d.Code,
                            Name = d.Name,
                            Content = d.Content,
                            IdPhongBan = d.IdPhongBan,
                            PhongBan = pb != null ? pb.TenPhongBan : null,
                            IdStaffReception = d.IdStaffReception,
                            StaffReceptionName = s != null ? s.HoDem + " " + s.Ten : null,
                            TotalPerson = d.TotalPerson,
                            PhoneNumber = d.PhoneNumber,
                            Status = d.Status,
                            RequestDate = d.RequestDate,
                            ReceptionDate = d.ReceptionDate,
                            TotalMoney = d.TotalMoney
                        };

            var result = query.FirstOrDefault();
            return result;
        }
        public async Task<DetailDelegationIncomingResponseDto> GetByIdDetailDelegation(int id)
        {
            _logger.LogInformation($"{nameof(GetByIdDetailDelegation)} called with id: {id}");

            var detail = _unitOfWork.iDetailDelegationIncomingRepository.TableNoTracking
                .FirstOrDefault(d => d.Id == id);

            if (detail == null)
            {
                return null; 
            }

            var delegation = _unitOfWork.iDelegationIncomingRepository.TableNoTracking
                .FirstOrDefault(d => d.Id == detail.DelegationIncomingId);
           
            var result = new DetailDelegationIncomingResponseDto
            {
                Id = detail.Id,
                Code = detail.Code,
                FirstName = detail.FirstName,
                LastName = detail.LastName,
                YearOfBirth = detail.YearOfBirth,
                PhoneNumber = detail.PhoneNumber,
                Email = detail.Email,
                IsLeader = detail.IsLeader,
                DelegationIncomingId = detail.DelegationIncomingId,
                DelegationName = delegation != null ? delegation.Name : null, 
                DelegationCode = delegation != null ? delegation.Code : null
            };

            return result;
        }
        public async Task<ReceptionTimeResponseDto> GetByIdReceptionTime(int id)
        {
            _logger.LogInformation($"{nameof(GetByIdReceptionTime)} called with id: {id}");

            var receptionTime = _unitOfWork.iReceptionTimeRepository.TableNoTracking
                .FirstOrDefault(r => r.Id == id);

            if (receptionTime == null)
                return null; 

            var delegation = _unitOfWork.iDelegationIncomingRepository.TableNoTracking
                .FirstOrDefault(d => d.Id == receptionTime.DelegationIncomingId);

            var result = new ReceptionTimeResponseDto
            {
                Id = receptionTime.Id,
                StartDate = receptionTime.StartDate,
                EndDate = receptionTime.EndDate,
                Date = receptionTime.Date,
                Content = receptionTime.Content,
                TotalPerson = receptionTime.TotalPerson,
                Address = receptionTime.Address,
                DelegationIncomingId = receptionTime.DelegationIncomingId,
                DelegationName = delegation != null ? delegation.Name : null,
                DelegationCode = delegation != null ? delegation.Code : null
            };

            return result;
        }






    }
}
