using DeliveryOffice.Core.Models;
using DeliveryOffice.DataAccess.Abstractions;
using DeliveryOffice.DataAccess.Repositories.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DeliveryOffice.DataAccess.Repositories;

public class BuyerReaderRepository : IBuyerReaderRepository
{
    private readonly IDbReader reader;

    public BuyerReaderRepository(IDbReader reader)
    {
        this.reader = reader;
    }

    public async Task<List<Buyer>> GetAllWithBillsAsync(CancellationToken cancellationToken)
    {
        var result = await reader.Read<Buyer>()
                                 .NotDeleted()
                                 .IgnoreAutoIncludes()
                                 .Include(s => s.Bills)
                                 .ToListAsync(cancellationToken);

        foreach (var buyer in result)
        {
            buyer.Bills = buyer.Bills
                               .AsQueryable()
                               .NotDeleted()
                               .ToList();
        }

        return result;
    }

    public async Task<Buyer?> GetByIdWithBillAsync(Guid id, CancellationToken cancellationToken)
    {
        var buyer = await reader.Read<Buyer>()
                                .ById(id)
                                .NotDeleted()
                                .Include(s => s.Bills)
                                .FirstOrDefaultAsync(cancellationToken);

        if (buyer is null)
            return buyer;

        buyer.Bills = buyer.Bills.AsQueryable().NotDeleted().ToList();
        return buyer;
    }
}
