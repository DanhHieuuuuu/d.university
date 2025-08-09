using D.Auth.Domain.Entities;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Infrastructure.Repositories
{
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(IDbContext dbContext, IHttpContextAccessor httpContext) : base(dbContext, httpContext)
        {
        }
    }

    public interface IStudentRepository : IRepositoryBase<Student> { }
}
