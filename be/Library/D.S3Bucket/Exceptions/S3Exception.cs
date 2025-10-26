using D.ControllerBase.Exceptions;

namespace D.S3Bucket.Exceptions
{
    public class S3Exception : BaseException
    {
        public S3Exception(int errorCode) : base(errorCode)
        {
        }

        public S3Exception(int errorCode, string message) : base(errorCode, message)
        {
        }
    }
}
