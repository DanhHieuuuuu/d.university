using D.DomainBase.Entity;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.InfrastructureBase.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        private readonly DbSet<T> _dbSet;

        private DbSet<T> _entities;
        public IDbContext _dbContext { get; }

        private IHttpContextAccessor _httpContext;
        //private string user;

        public RepositoryBase(IDbContext dbContext, IHttpContextAccessor httpContext)
        {
            _dbSet = dbContext.Set<T>();
            _dbContext = dbContext;
            _httpContext = httpContext;
            //user = CommonUntil.GetCurrentUserId(_httpContext).ToString();
        }

        public virtual IQueryable<T> TableNoTracking
        {
            get
            {
                return Entities.Where(x => x.Deleted == false).AsNoTracking();
            }
        }

        protected virtual DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _dbContext.Set<T>();
                return _entities;
            }
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return Entities;
            }
        }

        public async Task<T> AddAsync(T entity)
        {
            var userId = CommonUntil.GetCurrentUserId(_httpContext).ToString();
            var now = DateTime.Now;
            SetAuditInfo(entity, userId, now);
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            var userId = CommonUntil.GetCurrentUserId(_httpContext).ToString();
            var now = DateTime.Now;
            foreach (var entity in entities)
            {
                SetAuditInfo(entity, userId, now);
            }
            await _dbSet.AddRangeAsync(entities);
            return entities;
        }

        public void Add(T entity)
        {
            var userId = CommonUntil.GetCurrentUserId(_httpContext).ToString();
            var now = DateTime.Now;
            SetAuditInfo(entity, userId, now);
            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            var userId = CommonUntil.GetCurrentUserId(_httpContext).ToString();
            var now = DateTime.Now;
            foreach (var entity in entities)
            {
                SetAuditInfo(entity, userId, now);
            }
            _dbSet.AddRange(entities);
        }

        /// <summary>
        /// Gán CreatedBy/CreatedDate cho entity và toàn bộ navigation con
        /// </summary>
        private void SetAuditInfo(object entity, String userId, DateTime now)
        {

            if (entity is EntityBase baseEntity)
            {
                baseEntity.CreatedBy = userId;
                baseEntity.CreatedDate = now;
            }

            // Duyệt qua navigation collection
            var properties = entity.GetType().GetProperties()
                .Where(p => typeof(System.Collections.IEnumerable).IsAssignableFrom(p.PropertyType) && p.PropertyType != typeof(string));

            foreach (var prop in properties)
            {
                if (prop.GetValue(entity) is System.Collections.IEnumerable collection)
                {
                    foreach (var item in collection)
                    {
                        SetAuditInfo(item, userId, now);
                    }
                }
            }
        }


        public void Update(T entity)
        {
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = CommonUntil.GetCurrentUserId(_httpContext).ToString(); ;
            _dbSet.Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.ModifiedDate = DateTime.Now;
                entity.ModifiedBy = CommonUntil.GetCurrentUserId(_httpContext).ToString(); ;
            }
            _dbSet.UpdateRange(entities);
        }

        public void Delete(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            entity.Deleted = true;
            entity.DeletedDate = DateTime.Now;
            entity.DeletedBy = CommonUntil.GetCurrentUserId(_httpContext).ToString(); ;
        }

        public void DeleteRange(IEnumerable<T> entitys)
        {
            foreach (var entity in entitys)
                Delete(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public int SaveChange()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangeAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public T FindById(int id)
        {
            return _dbSet.Find(id);
        }
    }
}
