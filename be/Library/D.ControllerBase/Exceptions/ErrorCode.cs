using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.ControllerBases.Exceptions
{
    public class ErrorCode
    {
        //Các mã lỗi căn bản
        public const int System = 1;
        public const int BadRequest = 400;
        public const int Unauthorized = 401;
        public const int NotFound = 404;
        public const int InternalServerError = 500;
        public const int CaptchaInvalid = 501;
        public const int CandidateLoginInvalid = 502;
    }
}
