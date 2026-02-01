using D.Core.Domain.Entities.SinhVien;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace D.Core.Infrastructure.Repositories.SinhVien
{
    public class SvChatbotModelRepository : RepositoryBase<SvChatbotModel>, ISvChatbotModelRepository
    {
        public SvChatbotModelRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }

        public async Task<List<SvChatbotModel>> GetAllAsync()
        {
            return await TableNoTracking
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        public async Task<SvChatbotModel?> GetByIdAsync(int id)
        {
            return await TableNoTracking
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<SvChatbotModel?> GetSelectedAsync()
        {
            return await TableNoTracking
                .FirstOrDefaultAsync(x => x.IsSelected);
        }

        public async Task ResetAllSelectedAsync()
        {
            var selectedModels = await Table
                .Where(x => x.IsSelected)
                .ToListAsync();

            foreach (var model in selectedModels)
            {
                model.IsSelected = false;
            }
        }
    }

    public interface ISvChatbotModelRepository : IRepositoryBase<SvChatbotModel>
    {
        Task<List<SvChatbotModel>> GetAllAsync();
        Task<SvChatbotModel?> GetByIdAsync(int id);
        Task<SvChatbotModel?> GetSelectedAsync();
        Task ResetAllSelectedAsync();
    }
}
