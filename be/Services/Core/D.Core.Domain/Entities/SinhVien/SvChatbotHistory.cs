using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace D.Core.Domain.Entities.SinhVien
{
    [Table(nameof(SvChatbotHistory), Schema = DbSchema.Sv)]
    public class SvChatbotHistory : EntityBase
    {
        public string? SessionId { get; set; }
        public string? Mssv { get; set; }
        public string? Title { get; set; }
        public string? Role { get; set; }
        public string? Content { get; set; }
        public DateTime? LastAccess { get; set; }
    }
}
