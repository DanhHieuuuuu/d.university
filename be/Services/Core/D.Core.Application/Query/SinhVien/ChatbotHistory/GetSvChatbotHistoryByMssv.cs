using D.ApplicationBase;
using D.Core.Domain.Dtos.SinhVien.ChatbotHistory;
using D.Core.Infrastructure.Repositories.SinhVien;

namespace D.Core.Application.Query.SinhVien.ChatbotHistory
{
    public class GetSvChatbotHistoryByMssv : IQueryHandler<GetSvChatbotHistoryByMssvDto, List<SvChatbotHistoryResponseDto>>
    {
        private readonly ISvChatbotHistoryRepository _repository;

        public GetSvChatbotHistoryByMssv(ISvChatbotHistoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SvChatbotHistoryResponseDto>> Handle(
            GetSvChatbotHistoryByMssvDto req,
            CancellationToken cancellationToken
        )
        {
            var list = await _repository.GetByMssvAsync(req.Mssv ?? "");

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
