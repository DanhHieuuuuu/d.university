using D.ApplicationBase;
using D.Core.Domain.Dtos.SinhVien.ChatbotHistory;
using D.Core.Domain.Entities.SinhVien;
using D.Core.Infrastructure.Repositories.SinhVien;
using D.Untils.DataUntils;

namespace D.Core.Application.Command.SinhVien.ChatbotModel
{
    public class CreateSvChatbotModel : ICommandHandler<CreateSvChatbotModelDto, SvChatbotModelResponseDto>
    {
        private readonly ISvChatbotModelRepository _repository;

        public CreateSvChatbotModel(ISvChatbotModelRepository repository)
        {
            _repository = repository;
        }

        public async Task<SvChatbotModelResponseDto> Handle(
            CreateSvChatbotModelDto req,
            CancellationToken cancellationToken
        )
        {
            if (req.IsSelected)
            {
                await _repository.ResetAllSelectedAsync();
            }

            // Encrypt API key before saving
            var encryptedApiKey = !string.IsNullOrEmpty(req.APIKey) 
                ? AesEncryption.Encrypt(req.APIKey) 
                : req.APIKey;

            var entity = new SvChatbotModel
            {
                Name = req.Name,
                Description = req.Description,
                BaseURL = req.BaseURL,
                APIKey = encryptedApiKey,
                ModelName = req.ModelName,
                IsLocal = req.IsLocal,
                IsSelected = req.IsSelected
            };

            await _repository.AddAsync(entity);
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
