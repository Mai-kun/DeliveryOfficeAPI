using DeliveryOffice.Core.Models;
using DeliveryOffice.DataAccess.Abstractions;
using DeliveryOffice.DataAccess.Repositories.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DeliveryOffice.DataAccess.Repositories;

/// <inheritdoc />
public class BillReaderRepository : IBillReaderRepository
{
    private readonly IDbReader reader;

    public BillReaderRepository(IDbReader reader)
    {
        this.reader = reader;
    }

    Task<List<Bill>> IBillReaderRepository.GetAllAsync(CancellationToken cancellationToken)
    {
        return reader.Read<Bill>()
                     .NotDeleted()
                     .Include(b => b.Supplier)
                     .Include(b => b.Buyer)
                     .Include(b => b.Products)
                     .ToListAsync(cancellationToken);
    }

    async Task<Bill?> IBillReaderRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await reader.Read<Bill>()
                     .ById(id)
                     .NotDeleted()
                     .Include(b => b.Supplier)
                     .Include(b => b.Buyer)
                     .Include(b => b.Products)
                     .FirstOrDefaultAsync(cancellationToken);
    }
}
