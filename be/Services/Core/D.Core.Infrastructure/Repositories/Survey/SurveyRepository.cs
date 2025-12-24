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
    public class SurveyRepository : RepositoryBase<KsSurvey>, IKsSurveyRepository
    {
        public SurveyRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }

        public async Task<List<KsSurveyQuestion>> GetQuestionsByRequestId(int requestId)
        {
            return await _dbContext.Set<KsSurveyQuestion>()
                .Include(q => q.Answers)
                .Where(q => q.IdYeuCau == requestId)
                .OrderBy(q => q.Id)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<CorrectAnswerDto>> GetCorrectAnswers(int requestId)
        {
            var query = from q in _dbContext.Set<KsSurveyQuestion>()
                        join a in _dbContext.Set<KsQuestionAnswer>()
                            on q.Id equals a.IdCauHoi

                        where q.IdYeuCau == requestId
                           && a.IsCorrect == true

                        select new CorrectAnswerDto
                        {
                            QuestionId = q.Id,
                            AnswerId = a.Id,
                            Score = 1 
                        };

            return await query.ToListAsync();
        }

    }

    public interface IKsSurveyRepository : IRepositoryBase<KsSurvey>
    {
        Task<List<KsSurveyQuestion>> GetQuestionsByRequestId(int requestId);
        Task<List<CorrectAnswerDto>> GetCorrectAnswers(int requestId);
    }
}

