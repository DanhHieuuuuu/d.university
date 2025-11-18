using D.DomainBase.Common;
using Microsoft.AspNetCore.Http;

namespace D.Auth.Domain.Dtos.User
{
    public class UpdateImageUserDto : ICommand<Stream>
    {
        public string MaNhanSu { get; set; }
        public IFormFile File { get; set; }
    }
}
