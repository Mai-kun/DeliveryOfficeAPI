using DeliveryOffice.Core.Models;
using DeliveryOffice.DataAccess.Abstractions;
using DeliveryOffice.DataAccess.Repositories.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DeliveryOffice.DataAccess.Repositories;

public class SupplierReaderRepository : ISupplierReaderRepository
{
    private readonly IDbReader reader;

    public SupplierReaderRepository(IDbReader reader)
    {
        this.reader = reader;
    }

    async Task<List<Supplier>> ISupplierReaderRepository.GetAllWithBillsAsync(CancellationToken cancellationToken)
    {
        var result = await reader.Read<Supplier>()
                                 .NotDeleted()
                                 .IgnoreAutoIncludes()
                                 .Include(s => s.Bills)
                                 .ToListAsync(cancellationToken);

        foreach (var supplier in result)
        {
            supplier.Bills = supplier.Bills
                                           .AsQueryable()
                                           .NotDeleted()
                                           .ToList();
        }

        return result;
    }

    async Task<Supplier?> ISupplierReaderRepository.GetByIdWithBillsAsync(Guid id, CancellationToken cancellationToken)
    {
        var supplier = await reader.Read<Supplier>()
                                   .ById(id)
                                   .NotDeleted()
                                   .Include(s => s.Bills)
                                   .FirstOrDefaultAsync(cancellationToken);
        if (supplier is null)
            return supplier;

        supplier.Bills = supplier.Bills.AsQueryable().NotDeleted().ToList();
        return supplier;
    }
}
