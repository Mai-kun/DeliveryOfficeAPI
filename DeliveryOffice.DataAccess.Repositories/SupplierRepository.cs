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

    async Task<List<Supplier>> ISupplierRepository.GetAllAsync()
    {
        return await dbContext.Suppliers
            .AsNoTracking()
            .ToListAsync();
    }

    async Task<Supplier?> ISupplierRepository.GetByIdAsync(Guid id)
    {
        return await dbContext.Suppliers
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    async Task ISupplierRepository.AddAsync(Supplier supplier)
    {
        await dbContext.AddAsync(supplier);
        await dbContext.SaveChangesAsync();
    }

    async Task ISupplierRepository.UpdateAsync(Supplier supplier)
    {
        dbContext.Suppliers.Update(supplier);
        await dbContext.SaveChangesAsync();
    }

    async Task<bool> ISupplierRepository.DeleteAsync(Guid id)
    {
        var supplier = await dbContext.Suppliers
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);
        if (supplier is null)
        {
            return false;
        }

        dbContext.Suppliers.Remove(supplier);
        await dbContext.SaveChangesAsync();
        return true;
    }
}
