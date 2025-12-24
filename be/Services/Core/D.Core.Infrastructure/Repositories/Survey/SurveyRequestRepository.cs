using D.Core.Domain.Dtos.Survey.Submit;
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

        public async Task<List<KsSurveyQuestion>> GetQuestionsByRequestIdAsync(int requestId)
        {
            return await _dbContext.Set<KsSurveyQuestion>()
                .Include(q => q.Answers)
                .Where(q => q.IdYeuCau == requestId) // Lưu ý: Check tên cột FK trong DB (IdRequest hay SurveyRequestId?)
                .OrderBy(q => q.Id)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<CorrectAnswerDto>> GetCorrectAnswersAsync(int requestId)
        {
            var query = from q in _dbContext.Set<KsSurveyQuestion>()
                        join a in _dbContext.Set<KsQuestionAnswer>()
                            on q.Id equals a.IdCauHoi
                        where q.IdYeuCau == requestId && a.IsCorrect == true
                        select new CorrectAnswerDto
                        {
                            QuestionId = q.Id,
                            AnswerId = a.Id
                        };
            return await query.ToListAsync();
        }

    }

    public interface IKsSurveyRequestRepository : IRepositoryBase<KsSurveyRequest>
    {
        bool IsMaYeuCauExist(string code);
        Task<KsSurveyRequest> GetDetailWithNavigationsAsync(int id);
        Task<List<KsSurveyQuestion>> GetQuestionsByRequestIdAsync(int requestId);
        Task<List<CorrectAnswerDto>> GetCorrectAnswersAsync(int requestId);
    }
}
