using DeliveryOffice.Core.Abstractions;
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

    public async Task<List<Supplier>> GetAllAsync()
    {
        return await dbContext.Suppliers
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Supplier?> GetByIdAsync(Guid id)
    {
        return await dbContext.Suppliers
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task AddAsync(Supplier supplier)
    {
        await dbContext.AddAsync(supplier);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Supplier supplier)
    {
        dbContext.Suppliers.Update(supplier);
        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var supplier = await GetByIdAsync(id);
        if (supplier is null)
        {
            return false;
        }

        dbContext.Suppliers.Remove(supplier);
        await dbContext.SaveChangesAsync();
        return true;
    }
}
