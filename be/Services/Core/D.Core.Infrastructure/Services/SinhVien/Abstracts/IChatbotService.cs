using D.Core.Domain.Dtos.SinhVien.ChatbotHistory;

namespace D.Core.Infrastructure.Services.SinhVien.Abstracts
{
    public interface IChatbotService
    {
        Task<ChatbotResponseDto> ChatAsync(ChatbotRequestDto request);
        Task<SyncStudentsResponseDto> SyncAllStudentsAsync();
    }
}
