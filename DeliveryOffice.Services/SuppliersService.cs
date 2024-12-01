using DeliveryOffice.Core.Abstractions.Repositories;
using DeliveryOffice.Core.Models;
using DeliveryOffice.Services.Contracts.Abstractions;
using DeliveryOffice.Services.Contracts.Models.RequestModels;

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

    async Task ISuppliersService.AddSupplierAsync(CreateSupplierRequest supplierRequest)
    {
        var supplier = new Supplier { Name = supplierRequest.Name, Address = supplierRequest.Address, };
        await suppliersRepository.AddAsync(supplier);
    }

    async Task<bool> ISuppliersService.UpdateSupplierAsync(Guid id, UpdateSupplierRequest supplierRequest)
    {
        var supplier = await suppliersRepository.GetByIdAsync(id);
        if (supplier is null)
        {
            return false;
        }

        var newSupplier = new Supplier
        {
            Id = supplier.Id, Name = supplierRequest.Name ?? supplier.Name, Address = supplierRequest.Address ?? supplier.Address,
        };

        await suppliersRepository.UpdateAsync(newSupplier);
        return true;
    }

    async Task<bool> ISuppliersService.DeleteSupplierAsync(Guid id)
    {
        return await suppliersRepository.DeleteAsync(id);
    }
}
