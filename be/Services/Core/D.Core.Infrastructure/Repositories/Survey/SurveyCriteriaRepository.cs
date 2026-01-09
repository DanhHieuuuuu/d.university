using D.Core.Domain.Entities.Survey;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Repositories.Survey
{
    public class SurveyCriteriaRepository : RepositoryBase<KsSurveyCriteria>, IKsSurveyCriteriaRepository
    {
        public SurveyCriteriaRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }
    }

    public interface IKsSurveyCriteriaRepository : IRepositoryBase<KsSurveyCriteria>
    {
    }
}
