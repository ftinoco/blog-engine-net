using BlogEngineNet.Core.Domain.Persistence;
using BlogEngineNet.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data;

namespace BlogEngineNet.Persistence.Contexts;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public IDbConnection Connection => Database.GetDbConnection();

    public bool HasChanges => ChangeTracker.HasChanges();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ModelConfigurations.Config(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    public Task<int> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}