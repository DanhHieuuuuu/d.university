using AutoMapper;
using D.Constants.Core.Delegation;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming.Paging;
using D.Core.Domain.Entities.Delegation.Incoming;
using D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using D.InfrastructureBase.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Services.Delegation.Incoming.Implements
{
    public class SupporterService : ServiceBase, ISupporterService
    {
        private readonly ServiceUnitOfWork _unitOfWork;

        public SupporterService(
            ILogger<SupporterService> logger,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork
        )
            : base(logger, httpContext, mapper)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<CreateSupporterResponseDto> CreateSupporter(CreateSupporterRequestDto dto)
        {
            _logger.LogInformation($"{nameof(CreateSupporter)} method called, dto: {JsonSerializer.Serialize(dto)}.");
            var exist = _unitOfWork.iSupporterRepository.IsSupporterCodeExist(dto.SupporterCode!);
            if (exist)
                throw new Exception("SupporterCode đã tồn tại");

            //Kiểm tra supporterId có tồn tại trong nhân sự 
            var nhanSuExist = await _unitOfWork.iNsNhanSuRepository
                .TableNoTracking
                .AnyAsync(x => x.Id == dto.SupporterId && !x.Deleted);

            if (!nhanSuExist)
                throw new Exception("Không tồn tại nhân sự này");

            var newSupporter = _mapper.Map<Supporter>(dto);
            _unitOfWork.iSupporterRepository.Add(newSupporter);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CreateSupporterResponseDto>(newSupporter);

        }
        public PageResultDto<PageSupporterResultDto> PagingSupporter(FilterSupporterDto dto)
        {
            _logger.LogInformation($"{nameof(PagingSupporter)} method called, dto: {JsonSerializer.Serialize(dto)}.");

            var query =
                from s in _unitOfWork.iSupporterRepository.TableNoTracking
                join ns in _unitOfWork.iNsNhanSuRepository.TableNoTracking
                    on s.SupporterId equals ns.Id
                where !s.Deleted
                    && (string.IsNullOrWhiteSpace(dto.SupporterCode)
                        || s.SupporterCode.Contains(dto.SupporterCode))
                    && (!dto.DepartmentSupportId.HasValue
                        || s.DepartmentSupportId == dto.DepartmentSupportId.Value)
                select new PageSupporterResultDto
                {
                    Id = s.Id,
                    SupporterId = s.SupporterId,
                    SupporterName = ns.HoDem +" "+ ns.Ten, 
                    SupporterCode = s.SupporterCode,
                    DepartmentSupportId = s.DepartmentSupportId
                };

            var totalCount = query.Count();

            var items = query
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .ToList();

            return new PageResultDto<PageSupporterResultDto>
            {
                Items = items,
                TotalItem = totalCount
            };
        }

    }
}
