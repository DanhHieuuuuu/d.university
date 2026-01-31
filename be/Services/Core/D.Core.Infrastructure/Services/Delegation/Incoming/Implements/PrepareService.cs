using AutoMapper;
using D.Constants.Core.Delegation;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using D.Core.Domain.Entities.Delegation.Incoming;
using D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts;
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
    public class PrepareService : ServiceBase, IPrepareService
    {
        private readonly ServiceUnitOfWork _unitOfWork;

        public PrepareService(
            ILogger<PrepareService> logger,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork
        )
            : base(logger, httpContext, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CreatePrepareResponseDto>> CreatePrepareList(CreatePrepareRequestDto dto)
        {
            _logger.LogInformation($"{nameof(CreatePrepareList)} called, dto: {JsonSerializer.Serialize(dto)}");

            //Lấy danh sách ReceptionTimeId từ request
            var receptionTimeIds = dto.Items
                .Select(x => x.ReceptionTimeId)
                .Distinct()
                .ToList();
            //Lấy các ReceptionTime tồn tại trong DB
            var existingReceptionTimeIds = await _unitOfWork.iReceptionTimeRepository.TableNoTracking
                .Where(rt => receptionTimeIds.Contains(rt.Id))
                .Select(rt => rt.Id)
                .ToListAsync();
            // Kiểm tra ID không tồn tại
            var notFoundIds = receptionTimeIds.Except(existingReceptionTimeIds).ToList();
            if (notFoundIds.Any())
            {
                throw new Exception($"ReceptionTime không tồn tại: Id{string.Join(", ", notFoundIds)}");
            }
            var prepare = _mapper.Map<List<Prepare>>(dto.Items);
            _unitOfWork.iPrepareRepository.AddRange(prepare);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<List<CreatePrepareResponseDto>>(prepare);
        }
        public async Task<List<UpdatePrepareResponseDto>> UpdatePrepares(UpdatePrepareRequestDto dto)
        {
            if (dto.Items == null)
                throw new Exception("Danh sách prepare không hợp lệ.");

            //Check ReceptionTime tồn tại
            var receptionTimeExists = _unitOfWork.iReceptionTimeRepository
                .TableNoTracking
                .Any(x => x.Id == dto.ReceptionTimeId && !x.Deleted);

            if (!receptionTimeExists)
                throw new Exception("ReceptionTime không tồn tại.");

            //Load Prepare hiện tại
            var dbItems = _unitOfWork.iPrepareRepository.Table
                .Where(p => p.ReceptionTimeId == dto.ReceptionTimeId && !p.Deleted)
                .ToList();

            //Validate: FE gửi Id không tồn tại → lỗi
            var invalidIds = dto.Items
                .Select(i => i.Id)
                .Except(dbItems.Select(d => d.Id))
                .ToList();

            if (invalidIds.Any())
            {
                throw new Exception($"Prepare không tồn tại hoặc đã bị xoá: {string.Join(", ", invalidIds)}");
            }
            //UPDATE
            foreach (var item in dto.Items)
            {
                var exist = dbItems.First(x => x.Id == item.Id);

                exist.Name = item.Name;
                exist.Description = item.Description;
                exist.Money = item.Money;

                _unitOfWork.iPrepareRepository.Update(exist);
            }
            //SOFT DELETE
            var clientIds = dto.Items.Select(x => x.Id).ToList();

            var softDeleteItems = dbItems
                .Where(x => !clientIds.Contains(x.Id))
                .ToList();

            foreach (var item in softDeleteItems)
            {
                item.Deleted = true;
                _unitOfWork.iPrepareRepository.Update(item);
            }

            await _unitOfWork.SaveChangesAsync();
            var result = _unitOfWork.iPrepareRepository.Table
                .Where(p => p.ReceptionTimeId == dto.ReceptionTimeId && !p.Deleted)
                .ToList();

            return _mapper.Map<List<UpdatePrepareResponseDto>>(result);
        }


    }
}
