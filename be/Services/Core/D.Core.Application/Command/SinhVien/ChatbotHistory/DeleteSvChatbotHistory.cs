using D.ApplicationBase;
using D.Core.Domain.Dtos.SinhVien.ChatbotHistory;
using D.Core.Infrastructure.Repositories.SinhVien;
using Microsoft.EntityFrameworkCore;

namespace D.Core.Application.Command.SinhVien.ChatbotHistory
{
    public class DeleteSvChatbotHistory : ICommandHandler<DeleteSvChatbotHistoryDto, bool>
    {
        private readonly ISvChatbotHistoryRepository _repository;

        public DeleteSvChatbotHistory(ISvChatbotHistoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(
            DeleteSvChatbotHistoryDto req,
            CancellationToken cancellationToken
        )
        {
            if (string.IsNullOrEmpty(req.SessionId))
                throw new Exception("SessionId không được để trống!");

            var entities = await _repository.Table
                .Where(x => x.SessionId == req.SessionId)
                .ToListAsync(cancellationToken);

            if (!entities.Any())
                throw new Exception("Không tìm thấy lịch sử chatbot với SessionId này!");

            _repository.DeleteRange(entities);
            await _repository.SaveChangeAsync();

            return true;
        }
    }
}
