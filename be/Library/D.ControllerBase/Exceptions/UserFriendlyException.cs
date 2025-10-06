using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.ControllerBase.Exceptions
{
    public class UserFriendlyException : BaseException
    {
        public UserFriendlyException(int errorCode) : base(errorCode)
        {
        }

        public UserFriendlyException(int errorCode, string? messageLocalize) : base(errorCode, messageLocalize)
        {
        }
        public UserFriendlyException(int errorCode, params string[] listParam) : base(errorCode, listParam)
        {
        }
    }
}
