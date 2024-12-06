using DeliveryOffice.Core.Abstractions.Repositories;
using DeliveryOffice.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliveryOffice.DataAccess.Repositories;

public class SupplierRepository : ISupplierRepository
{
    private readonly DeliveryOfficeDbContext dbContext;

    public SupplierRepository(DeliveryOfficeDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    async Task<List<Supplier>> ISupplierRepository.GetAllWithBillsAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Suppliers
                              .AsNoTracking()
                              .IgnoreAutoIncludes()
                              .Include(s => s.Bills.Where(b => b.IsDeleted == false))
                              .ToListAsync(cancellationToken);
    }

    async Task<Supplier?> ISupplierRepository.GetByIdWithBillsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Suppliers
                              .AsNoTracking()
                              .IgnoreAutoIncludes()
                              .Include(s => s.Bills.Where(b => b.IsDeleted == false))
                              .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    async Task ISupplierRepository.AddAsync(Supplier supplier)
    {
        supplier.Id = Guid.NewGuid();
        supplier.CreatedAt = DateTime.Now;
        await dbContext.AddAsync(supplier);
        await dbContext.SaveChangesAsync();
    }

    async Task ISupplierRepository.UpdateAsync(Supplier supplier)
    {
        supplier.ModifiedAt = DateTime.Now;
        dbContext.Suppliers.Update(supplier);
        await dbContext.SaveChangesAsync();
    }

    async Task<bool> ISupplierRepository.DeleteAsync(Guid id)
    {
        var result = await dbContext.Suppliers
                                    .Where(s => s.Id == id)
                                    .ExecuteUpdateAsync(p =>
                                        p.SetProperty(s => s.IsDeleted, true));
        return result > 0;
    }
}
