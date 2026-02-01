using D.ApplicationBase;
using D.Core.Domain.Dtos.SinhVien.ChatbotHistory;
using D.Core.Infrastructure.Repositories.SinhVien;
using D.Untils.DataUntils;

namespace D.Core.Application.Command.SinhVien.ChatbotModel
{
    public class UpdateSvChatbotModel : ICommandHandler<UpdateSvChatbotModelDto, SvChatbotModelResponseDto>
    {
        private readonly ISvChatbotModelRepository _repository;

        public UpdateSvChatbotModel(ISvChatbotModelRepository repository)
        {
            _repository = repository;
        }

        public async Task<SvChatbotModelResponseDto> Handle(
            UpdateSvChatbotModelDto req,
            CancellationToken cancellationToken
        )
        {
            var entity = await _repository.GetByIdAsync(req.Id);
            if (entity == null)
            {
                throw new Exception($"Không tìm thấy ChatbotModel với Id = {req.Id}");
            }

            // Chỉ update IsSelected nếu có giá trị và thay đổi từ false -> true
            if (req.IsSelected.HasValue && req.IsSelected.Value && !entity.IsSelected)
            {
                await _repository.ResetAllSelectedAsync();
            }

            // Chỉ update các trường có giá trị (không null)
            if (req.Name != null)
                entity.Name = req.Name;
            
            if (req.Description != null)
                entity.Description = req.Description;
            
            if (req.BaseURL != null)
                entity.BaseURL = req.BaseURL;
            
            // Encrypt API key nếu có giá trị mới
            if (req.APIKey != null)
                entity.APIKey = AesEncryption.Encrypt(req.APIKey);
            
            if (req.ModelName != null)
                entity.ModelName = req.ModelName;
            
            if (req.IsLocal.HasValue)
                entity.IsLocal = req.IsLocal.Value;
            
            if (req.IsSelected.HasValue)
                entity.IsSelected = req.IsSelected.Value;

            _repository.Update(entity);
            await _repository.SaveChangeAsync();

            return new SvChatbotModelResponseDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                BaseURL = entity.BaseURL,
                APIKey = entity.APIKey,
                ModelName = entity.ModelName,
                IsLocal = entity.IsLocal,
                IsSelected = entity.IsSelected,
                CreatedDate = entity.CreatedDate
            };
        }
    }
}
