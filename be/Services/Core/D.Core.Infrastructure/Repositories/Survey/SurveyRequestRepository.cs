using D.Core.Domain.Entities.Survey;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Repositories.Survey
{
    public class SurveyRequestRepository : RepositoryBase<KsSurveyRequest>, IKsSurveyRequestRepository
    {
        public SurveyRequestRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }
        public bool IsMaYeuCauExist(string code)
        {
            return TableNoTracking.Any(x => x.MaYeuCau == code);
        }

        public async Task<KsSurveyRequest> GetDetailWithNavigationsAsync(int id)
        {
            return await TableNoTracking
                .Include(x => x.Targets)
                .Include(x => x.Criterias)
                .Include(x => x.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }

    public interface IKsSurveyRequestRepository : IRepositoryBase<KsSurveyRequest>
    {
        bool IsMaYeuCauExist(string code);
        Task<KsSurveyRequest> GetDetailWithNavigationsAsync(int id);
    }
}
