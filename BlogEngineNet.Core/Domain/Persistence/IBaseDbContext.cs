using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data;

namespace BlogEngineNet.Core.Domain.Persistence;

public interface IBaseDbContext
{
    IDbConnection Connection { get; }

    bool HasChanges { get; }

    EntityEntry Entry(object entity);

    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    Task<int> SaveChangesAsync();

    int SaveChanges();

    DbSet<TEntity> Set<TEntity>() where TEntity : class;
}