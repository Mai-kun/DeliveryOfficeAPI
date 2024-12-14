using DeliveryOffice.Core.Configurations;
using DeliveryOffice.Core.Models;
using DeliveryOffice.DataAccess.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DeliveryOffice.DataAccess;

public class DeliveryOfficeDbContext : DbContext, IDbWriter, IDbReader, IUnitOfWork
{
    /// <remarks>
    ///     dotnet tool install --global dotnet-ef --version 7.*
    ///     dotnet tool install --global dotnet-ef
    ///     dotnet ef migrations add Init --project DeliveryOffice.DataAccess\DeliveryOffice.DataAccess.csproj
    ///     dotnet ef database update --project DeliveryOffice.DataAccess\DeliveryOffice.DataAccess.csproj
    /// </remarks>
    public DeliveryOfficeDbContext(DbContextOptions<DeliveryOfficeDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConfigurationAnchor).Assembly);
    }

    public DbSet<Bill> Bills { get; init; }
    public DbSet<Buyer> Buyers { get; init; }
    public DbSet<Product> Products { get; init; }
    public DbSet<Supplier> Suppliers { get; init; }

    void IDbWriter.Add<TEntity>(TEntity entity)
        => Set<TEntity>().Entry(entity).State = EntityState.Added;

    void IDbWriter.Update<TEntity>(TEntity entity)
        => Set<TEntity>().Entry(entity).State = EntityState.Modified;

    void IDbWriter.Delete<TEntity>(TEntity entity)
        => Set<TEntity>().Entry(entity).State = EntityState.Deleted;

    IQueryable<TEntity> IDbReader.Read<TEntity>()
        => base.Set<TEntity>()
               .AsNoTracking()
               .AsQueryable();

    async Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
    {
        var count = await base.SaveChangesAsync(cancellationToken);
        foreach (var entry in base.ChangeTracker.Entries().ToArray())
        {
            entry.State = EntityState.Detached;
        }

        return count;
    }
}
