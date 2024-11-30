using DeliveryOffice.Core.Abstractions.Repositories;
using DeliveryOffice.Core.Models;
using DeliveryOffice.Services.Models;
using DeliveryOffice.Services.Models.Abstractions;
using DeliveryOffice.Services.Models.Models.RequestModels;

namespace DeliveryOffice.Services;

/// <inheritdoc cref="ISuppliersService" />
public class SuppliersService : ISuppliersService
{
    private readonly ISupplierRepository suppliersRepository;

    public SuppliersService(ISupplierRepository suppliersRepository)
    {
        this.suppliersRepository = suppliersRepository;
    }

    async Task<List<Supplier>> ISuppliersService.GetAllSuppliersAsync()
    {
        return await suppliersRepository.GetAllAsync();
    }

    async Task<Supplier?> ISuppliersService.GetSupplierByIdAsync(Guid supplierId)
    {
        return await suppliersRepository.GetByIdAsync(supplierId);
    }

    async Task ISuppliersService.AddSupplierAsync(SupplierRequest supplierModel)
    {
        var supplier = new Supplier
        {
            Name = supplierModel.Name,
            Address = supplierModel.Address,
        };
        await suppliersRepository.AddAsync(supplier);
    }

    Task ISuppliersService.UpdateSupplierAsync(Supplier supplier)
    {
        return suppliersRepository.UpdateAsync(supplier);
    }

    Task ISuppliersService.DeleteSupplierAsync(Supplier supplier)
    {
        return suppliersRepository.DeleteAsync(supplier);
    }
}
