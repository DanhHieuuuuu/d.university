using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.InfrastructureBase.Service
{
    public abstract class ServiceBase
    {
        protected readonly ILogger _logger;
        protected readonly IHttpContextAccessor _contextAccessor;
        protected readonly IMapper _mapper;
        protected ServiceBase(ILogger logger, IHttpContextAccessor contextAccessor, IMapper mapper)
        {
            _logger = logger;
            _contextAccessor = contextAccessor;
            _mapper = mapper;
        }
    }
}
