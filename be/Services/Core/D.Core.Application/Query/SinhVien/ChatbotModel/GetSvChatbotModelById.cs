using D.ApplicationBase;
using D.Core.Domain.Dtos.SinhVien.ChatbotHistory;
using D.Core.Infrastructure.Repositories.SinhVien;

namespace D.Core.Application.Query.SinhVien.ChatbotModel
{
    public class GetSvChatbotModelById : IQueryHandler<GetSvChatbotModelByIdDto, SvChatbotModelResponseDto?>
    {
        private readonly ISvChatbotModelRepository _repository;

        public GetSvChatbotModelById(ISvChatbotModelRepository repository)
        {
            _repository = repository;
        }

        public async Task<SvChatbotModelResponseDto?> Handle(
            GetSvChatbotModelByIdDto req,
            CancellationToken cancellationToken
        )
        {
            var entity = await _repository.GetByIdAsync(req.Id);
            
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
