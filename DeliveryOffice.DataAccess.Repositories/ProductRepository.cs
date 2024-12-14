using DeliveryOffice.API.Common.Abstractions;
using DeliveryOffice.Core.Abstractions.Repositories;
using DeliveryOffice.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliveryOffice.DataAccess.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DeliveryOfficeDbContext dbContext;
    private readonly IDateTimeProvider dateTimeProvider;

    public ProductRepository(DeliveryOfficeDbContext dbContext, IDateTimeProvider dateTimeProvider)
    {
        this.dbContext = dbContext;
        this.dateTimeProvider = dateTimeProvider;
    }

    async Task<List<Product>> IProductRepository.GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Products
                              .AsNoTracking()
                              .ToListAsync(cancellationToken);
    }

    async Task<Product?> IProductRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Products
                              .AsNoTracking()
                              .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    async Task IProductRepository.AddAsync(Product product)
    {
        product.CreatedAt = dateTimeProvider.UtcNow;
        await dbContext.Products.AddAsync(product);
        await dbContext.SaveChangesAsync();
    }

    Task IProductRepository.UpdateAsync(Product product)
    {
        product.ModifiedAt = dateTimeProvider.Now;
        dbContext.Products.Update(product);
        return dbContext.SaveChangesAsync();
    }

    Task IProductRepository.DeleteAsync(Guid id)
    {
        return dbContext.Products
                        .Where(s => s.Id == id)
                        .ExecuteUpdateAsync(
                            p =>
                                p.SetProperty(s => s.IsDeleted, true));
    }
}
