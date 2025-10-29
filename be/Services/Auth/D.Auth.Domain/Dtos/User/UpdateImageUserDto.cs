using D.DomainBase.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Domain.Dtos.User
{
    public class UpdateImageUserDto : ICommand<Stream>
    {
        public string MaNhanSu { get; set; }
        public IFormFile File { get; set; }
    }
}
