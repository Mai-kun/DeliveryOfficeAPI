using DeliveryOffice.Core.Models;
using DeliveryOffice.DataAccess.Abstractions;
using DeliveryOffice.DataAccess.Repositories.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DeliveryOffice.DataAccess.Repositories;

public class ProductReaderRepository : IProductReaderRepository
{
    private readonly IDbReader reader;

    public ProductReaderRepository(IDbReader reader)
    {
        this.reader = reader;
    }

    Task<List<Product>> IProductReaderRepository.GetAllAsync(CancellationToken cancellationToken)
    {
        return reader.Read<Product>()
                     .NotDeleted()
                     .ToListAsync(cancellationToken);
    }

    Task<Product?> IProductReaderRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return reader.Read<Product>()
                     .ById(id)
                     .NotDeleted()
                     .FirstOrDefaultAsync(cancellationToken);
    }
}
