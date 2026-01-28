using D.ApplicationBase;
using D.Core.Domain.Dtos.SinhVien.ChatbotHistory;
using D.Core.Domain.Entities.SinhVien;
using D.Core.Infrastructure.Repositories.SinhVien;

namespace D.Core.Application.Command.SinhVien.ChatbotHistory
{
    public class CreateSvChatbotHistory : ICommandHandler<CreateSvChatbotHistoryDto, SvChatbotHistoryResponseDto>
    {
        private readonly ISvChatbotHistoryRepository _repository;

        public CreateSvChatbotHistory(ISvChatbotHistoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<SvChatbotHistoryResponseDto> Handle(
            CreateSvChatbotHistoryDto req,
            CancellationToken cancellationToken
        )
        {
            var entity = new SvChatbotHistory
            {
                SessionId = req.SessionId,
                Mssv = req.Mssv,
                Title = req.Title,
                Role = req.Role,
                Content = req.Content,
                LastAccess = DateTime.Now
            };

            await _repository.AddAsync(entity);
            await _repository.SaveChangeAsync();

            return new SvChatbotHistoryResponseDto
            {
                Id = entity.Id,
                SessionId = entity.SessionId,
                Mssv = entity.Mssv,
                Title = entity.Title,
                Role = entity.Role,
                Content = entity.Content,
                LastAccess = entity.LastAccess,
                CreatedDate = entity.CreatedDate
            };
        }
    }
}
