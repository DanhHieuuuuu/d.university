using D.DomainBase.Common;
using Microsoft.AspNetCore.Http;

namespace D.Core.Domain.Dtos.File
{
    public class UpdateFileDto : ICommand<bool>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IFormFile? File { get; set; }
        public string? ApplicationField { get; set; }
    }
}
