using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.InfrastructureBase.Database
{
    public interface IDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        EntityEntry Entry(object entity);

        ChangeTracker ChangeTracker { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync();

        DatabaseFacade Database { get; }
    }
}
