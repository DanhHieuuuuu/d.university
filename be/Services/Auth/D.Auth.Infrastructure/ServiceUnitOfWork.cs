using D.Auth.Infrastructure.Repositories;
using D.InfrastructureBase.Database;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Infrastructure
{
    public class ServiceUnitOfWork
    {
        private IDbContext _dbContext;
        private IHttpContextAccessor _httpContext;

        public ServiceUnitOfWork(IDbContext dbContext, IHttpContextAccessor httpContext)
        {
            _dbContext = dbContext;
            _httpContext = httpContext;
        }

        private StudentRepository _studentRepository;

        public IStudentRepository iStudentRepository
        {
            get
            {
                if(_studentRepository == null)
                {
                    _studentRepository = new StudentRepository(_dbContext, _httpContext);
                }
                return _studentRepository;
            }
        }
    }
}
