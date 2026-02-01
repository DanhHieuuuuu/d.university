using D.ApplicationBase;
using D.Core.Domain.Dtos.SinhVien.ChatbotHistory;
using D.Core.Infrastructure.Repositories.SinhVien;

namespace D.Core.Application.Query.SinhVien.ChatbotModel
{
    public class GetAllSvChatbotModel : IQueryHandler<GetAllSvChatbotModelDto, List<SvChatbotModelResponseDto>>
    {
        private readonly ISvChatbotModelRepository _repository;

        public GetAllSvChatbotModel(ISvChatbotModelRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SvChatbotModelResponseDto>> Handle(
            GetAllSvChatbotModelDto req,
            CancellationToken cancellationToken
        )
        {
            var list = await _repository.GetAllAsync();

            return list.Select(x => new SvChatbotModelResponseDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                BaseURL = x.BaseURL,
                APIKey = x.APIKey,
                ModelName = x.ModelName,
                IsLocal = x.IsLocal,
                IsSelected = x.IsSelected,
                CreatedDate = x.CreatedDate
            }).ToList();
        }
    }
}
