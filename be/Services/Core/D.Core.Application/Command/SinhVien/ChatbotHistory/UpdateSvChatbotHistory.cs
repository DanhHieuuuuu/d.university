using D.ApplicationBase;
using D.Core.Domain.Dtos.SinhVien.ChatbotHistory;
using D.Core.Infrastructure.Repositories.SinhVien;

namespace D.Core.Application.Command.SinhVien.ChatbotHistory
{
    public class UpdateSvChatbotHistory : ICommandHandler<UpdateSvChatbotHistoryDto, SvChatbotHistoryResponseDto>
    {
        private readonly ISvChatbotHistoryRepository _repository;

        public UpdateSvChatbotHistory(ISvChatbotHistoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<SvChatbotHistoryResponseDto> Handle(
            UpdateSvChatbotHistoryDto req,
            CancellationToken cancellationToken
        )
        {
            var entity = _repository.FindById(req.Id);
            if (entity == null)
                throw new Exception("Không tìm thấy lịch sử chatbot!");

            if (!string.IsNullOrEmpty(req.Title))
                entity.Title = req.Title;
            if (!string.IsNullOrEmpty(req.Role))
                entity.Role = req.Role;
            if (!string.IsNullOrEmpty(req.Content))
                entity.Content = req.Content;

            entity.LastAccess = DateTime.Now;

            _repository.Update(entity);
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
