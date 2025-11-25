using AutoMapper;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming.Paging;
using D.Core.Domain.Entities.Delegation.Incoming;
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
                string.IsNullOrEmpty(dto.Keyword)
                || x.Name.ToLower().Contains(dto.Keyword.ToLower())
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

            var entity = _mapper.Map<DelegationIncoming>(dto);

            _unitOfWork.iDelegationIncomingRepository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CreateResponseDto>(entity);
        }
    }
}
