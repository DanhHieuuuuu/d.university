using AutoMapper;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming.Paging;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
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
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace D.Core.Infrastructure.Services.Delegation.Incoming.Implements
{
    public class DepartmentSupportService : ServiceBase, IDepartmentSupportService
    {
        private readonly ServiceUnitOfWork _unitOfWork;

        public DepartmentSupportService(
            ILogger<DepartmentSupportService> logger,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork
        )
            : base(logger, httpContext, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateDepartmentSupportResponseDto> CreateDepartmentSupport(CreateDepartmentSupportRequestDto dto)
        {
            _logger.LogInformation($"{nameof(CreateDepartmentSupport)} method called, dto: {JsonSerializer.Serialize(dto)}.");

            //Kiểm tra departmentSupportId có tồn tại trong phòng ban 
            var phongBanExist = await _unitOfWork.iDmPhongBanRepository
                .TableNoTracking
                .AnyAsync(x => x.Id == dto.DepartmentSupportId && !x.Deleted);

            if (!phongBanExist)
                throw new Exception("Không tồn tại phòng ban này");

            var newSupporter = _mapper.Map<DepartmentSupport>(dto);
            _unitOfWork.iDepartmentSupportRepository.Add(newSupporter);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CreateDepartmentSupportResponseDto>(newSupporter);

        }
        public PageResultDto<PageDepartmentSupportResultDto> PagingDepartmentSupport(FilterDepartmentSupportDto dto)
        {
            _logger.LogInformation($"{nameof(PagingDepartmentSupport)} method called, dto: {JsonSerializer.Serialize(dto)}.");

            var department = _unitOfWork.iDepartmentSupportRepository.TableNoTracking.Include(x => x.Supporters).Include(x => x.DelegationIncoming);

            var query =
                from ds in department
                join pb in _unitOfWork.iDmPhongBanRepository.TableNoTracking
                    on ds.DepartmentSupportId equals pb.Id
                where !ds.Deleted
                select new PageDepartmentSupportResultDto
                {
                    Id = ds.Id,
                    DepartmentSupportId = pb.Id,
                    DepartmentSupportName = pb.TenPhongBan,
                    DelegationIncomingId = ds.DelegationIncoming.Id,
                    DelegationIncomingName = ds.DelegationIncoming.Name,
                    Content = ds.Content,
                    Supporters = ds.Supporters.ToList(),
                };

            var totalCount = query.Count();

            var items = query
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .ToList();

            return new PageResultDto<PageDepartmentSupportResultDto>
            {
                Items = items,
                TotalItem = totalCount
            };
        }


    }
}
