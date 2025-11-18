using D.DomainBase.Entity;

namespace D.InfrastructureBase.Repository
{
    public interface IRepositoryBase<Entity> where Entity : EntityBase
    {
        Task<Entity> AddAsync(Entity entity);
        Task<IEnumerable<Entity>> AddRangeAsync(IEnumerable<Entity> entity);
        void Add(Entity entity);
        void AddRange(IEnumerable<Entity> entities);
        void Update(Entity entity);
        void UpdateRange(IEnumerable<Entity> entitys);
        void Delete(Entity entity);
        void DeleteRange(IEnumerable<Entity> entitys);
        void Remove(Entity entity);
        void RemoveRange(IEnumerable<Entity> entitys);
        int SaveChange();
        Task<int> SaveChangeAsync();
        Entity FindById(int id);

        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<Entity> Table { get; }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        IQueryable<Entity> TableNoTracking { get; }
    }
}
