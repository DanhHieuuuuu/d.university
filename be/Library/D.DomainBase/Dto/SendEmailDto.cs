using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.DomainBase.Dto
{
    public class SendEmailDto
    {
        public string EmailFrom { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Post { get; set; }
        public string EmailTo { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
