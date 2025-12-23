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
        public async Task<List<CreateSupporterResponseDto>> CreateSupporter(CreateSupporterRequestDto dto)
        {
            _logger.LogInformation($"{nameof(CreateSupporter)} called.");

            var result = new List<Supporter>();

            foreach (var item in dto.Supporters)
            {
                // Check mã supporter
                if (!string.IsNullOrEmpty(item.SupporterCode))
                {
                    var existCode = _unitOfWork.iSupporterRepository
                        .IsSupporterCodeExist(item.SupporterCode);

                    if (existCode)
                        throw new Exception($"SupporterCode {item.SupporterCode} đã tồn tại");
                }

                //Check nhân sự tồn tại
                var nhanSuExist = await _unitOfWork.iNsNhanSuRepository
                    .TableNoTracking
                    .AnyAsync(x => x.Id == item.SupporterId && !x.Deleted);

                if (!nhanSuExist)
                    throw new Exception($"Không tồn tại nhân sự ID = {item.SupporterId}");

                // 3. Create entity
                var supporter = new Supporter
                {
                    SupporterId = item.SupporterId,
                    SupporterCode = item.SupporterCode,
                    DepartmentSupportId = dto.DepartmentSupportId
                };

                result.Add(supporter);
                _unitOfWork.iSupporterRepository.Add(supporter);
            }

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<List<CreateSupporterResponseDto>>(result);
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
