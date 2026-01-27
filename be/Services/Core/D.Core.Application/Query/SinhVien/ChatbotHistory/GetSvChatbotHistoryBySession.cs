using D.ApplicationBase;
using D.Core.Domain.Dtos.SinhVien.ChatbotHistory;
using D.Core.Infrastructure.Repositories.SinhVien;

namespace D.Core.Application.Query.SinhVien.ChatbotHistory
{
    public class GetSvChatbotHistoryBySession : IQueryHandler<GetSvChatbotHistoryBySessionDto, List<SvChatbotHistoryResponseDto>>
    {
        private readonly ISvChatbotHistoryRepository _repository;

        public GetSvChatbotHistoryBySession(ISvChatbotHistoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SvChatbotHistoryResponseDto>> Handle(
            GetSvChatbotHistoryBySessionDto req,
            CancellationToken cancellationToken
        )
        {
            var list = await _repository.GetBySessionIdAsync(req.SessionId ?? "");

            return list.Select(x => new SvChatbotHistoryResponseDto
            {
                Id = x.Id,
                SessionId = x.SessionId,
                Mssv = x.Mssv,
                Title = x.Title,
                Role = x.Role,
                Content = x.Content,
                LastAccess = x.LastAccess,
                CreatedDate = x.CreatedDate
            }).ToList();
        }
    }
}
