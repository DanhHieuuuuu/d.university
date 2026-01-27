using D.ApplicationBase;
using D.Core.Domain.Dtos.SinhVien.ChatbotHistory;
using D.Core.Infrastructure.Repositories.SinhVien;

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
            var entity = _repository.FindById(req.Id);
            if (entity == null)
                throw new Exception("Không tìm thấy lịch sử chatbot!");

            _repository.Delete(entity);
            await _repository.SaveChangeAsync();

            return true;
        }
    }
}
