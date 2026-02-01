using D.ApplicationBase;
using D.Core.Domain.Dtos.SinhVien.ChatbotHistory;
using D.Core.Infrastructure.Repositories.SinhVien;

namespace D.Core.Application.Query.SinhVien.ChatbotModel
{
    public class GetSelectedSvChatbotModel : IQueryHandler<GetSelectedSvChatbotModelDto, SvChatbotModelResponseDto?>
    {
        private readonly ISvChatbotModelRepository _repository;

        public GetSelectedSvChatbotModel(ISvChatbotModelRepository repository)
        {
            _repository = repository;
        }

        public async Task<SvChatbotModelResponseDto?> Handle(
            GetSelectedSvChatbotModelDto req,
            CancellationToken cancellationToken
        )
        {
            var entity = await _repository.GetSelectedAsync();
            
            if (entity == null)
                return null;

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
