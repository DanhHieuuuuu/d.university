using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Entities.Survey.Constants
{
    public static class RequestStatus
    {
        public const int Draft = 1;
        public const int Pending = 2;
        public const int Approved = 3;
        public const int Rejected = 4;
    }
}
