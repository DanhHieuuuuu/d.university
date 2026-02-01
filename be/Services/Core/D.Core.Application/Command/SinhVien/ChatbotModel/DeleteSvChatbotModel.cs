using D.ApplicationBase;
using D.Core.Domain.Dtos.SinhVien.ChatbotHistory;
using D.Core.Infrastructure.Repositories.SinhVien;

namespace D.Core.Application.Command.SinhVien.ChatbotModel
{
    public class DeleteSvChatbotModel : ICommandHandler<DeleteSvChatbotModelDto, bool>
    {
        private readonly ISvChatbotModelRepository _repository;

        public DeleteSvChatbotModel(ISvChatbotModelRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(
            DeleteSvChatbotModelDto req,
            CancellationToken cancellationToken
        )
        {
            var entity = await _repository.GetByIdAsync(req.Id);
            if (entity == null)
            {
                throw new Exception($"Không tìm thấy ChatbotModel với Id = {req.Id}");
            }

            _repository.Delete(entity);
            await _repository.SaveChangeAsync();

            return true;
        }
    }
}
