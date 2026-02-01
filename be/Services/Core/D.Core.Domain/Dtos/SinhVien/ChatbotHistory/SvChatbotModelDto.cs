using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.SinhVien.ChatbotHistory
{
    public class SvChatbotModelResponseDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? BaseURL { get; set; }
        public string? APIKey { get; set; }
        public string? ModelName { get; set; }
        public bool IsLocal { get; set; }
        public bool IsSelected { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

    public class CreateSvChatbotModelDto : ICommand<SvChatbotModelResponseDto>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? BaseURL { get; set; }
        public string? APIKey { get; set; }
        public string? ModelName { get; set; }
        public bool IsLocal { get; set; }
        public bool IsSelected { get; set; }
    }

    public class UpdateSvChatbotModelDto : ICommand<SvChatbotModelResponseDto>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? BaseURL { get; set; }
        public string? APIKey { get; set; }
        public string? ModelName { get; set; }
        public bool? IsLocal { get; set; }
        public bool? IsSelected { get; set; }
    }

    public class DeleteSvChatbotModelDto : ICommand<bool>
    {
        public int Id { get; set; }
    }

    public class GetAllSvChatbotModelDto : IQuery<List<SvChatbotModelResponseDto>>
    {
    }

    public class GetSvChatbotModelByIdDto : IQuery<SvChatbotModelResponseDto?>
    {
        public int Id { get; set; }
    }

    public class GetSelectedSvChatbotModelDto : IQuery<SvChatbotModelResponseDto?>
    {
    }
}
