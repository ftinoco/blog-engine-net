using BlogEngineNet.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data;

namespace BlogEngineNet.Core.Domain.Persistence;

public interface IApplicationDbContext
{
    IDbConnection Connection { get; }

    bool HasChanges { get; }

    EntityEntry Entry(object entity);

    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    Task<int> SaveChangesAsync();

    int SaveChanges();

    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    DbSet<User> Users { get; set; }
    DbSet<Post> Posts { get; set; }
    DbSet<Comment> Comments { get; set; }
    DbSet<PostTracking> PostTrackings { get; set; }
}