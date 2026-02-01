using D.Core.Domain.Shared.Constants;
using D.DomainBase.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Entities.SinhVien
{
    [Table(nameof(SvChatbotModel), Schema = DbSchema.Sv)]
    public class SvChatbotModel: EntityBase
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? BaseURL { get; set; }
        public string? APIKey { get; set; }
        public string? ModelName { get; set; }
        public bool IsLocal { get; set; }
        public bool IsSelected { get; set; }
    }
}
