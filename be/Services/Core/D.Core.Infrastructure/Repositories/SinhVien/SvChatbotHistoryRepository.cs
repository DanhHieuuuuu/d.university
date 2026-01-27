using D.Core.Domain.Entities.SinhVien;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace D.Core.Infrastructure.Repositories.SinhVien
{
    public class SvChatbotHistoryRepository : RepositoryBase<SvChatbotHistory>, ISvChatbotHistoryRepository
    {
        public SvChatbotHistoryRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }

        public async Task<List<SvChatbotHistory>> GetBySessionIdAsync(string sessionId)
        {
            return await TableNoTracking
                .Where(x => x.SessionId == sessionId)
                .OrderBy(x => x.CreatedDate)
                .ToListAsync();
        }

        public async Task<List<SvChatbotHistory>> GetByMssvAsync(string mssv)
        {
            return await TableNoTracking
                .Where(x => x.Mssv == mssv)
                .OrderByDescending(x => x.LastAccess)
                .ToListAsync();
        }

        public async Task<List<SvChatbotHistory>> GetSessionsByMssvAsync(string mssv)
        {
            return await TableNoTracking
                .Where(x => x.Mssv == mssv)
                .GroupBy(x => x.SessionId)
                .Select(g => g.OrderByDescending(x => x.LastAccess).First())
                .ToListAsync();
        }
    }

    public interface ISvChatbotHistoryRepository : IRepositoryBase<SvChatbotHistory>
    {
        Task<List<SvChatbotHistory>> GetBySessionIdAsync(string sessionId);
        Task<List<SvChatbotHistory>> GetByMssvAsync(string mssv);
        Task<List<SvChatbotHistory>> GetSessionsByMssvAsync(string mssv);
    }
}
