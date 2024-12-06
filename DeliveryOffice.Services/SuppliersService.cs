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

    async Task<IEnumerable<Supplier>> ISuppliersService.GetAllSuppliersAsync(CancellationToken cancellationToken)
    {
        var result = await suppliersRepository.GetAllWithBillsAsync(cancellationToken);
        return result.Where(s => s.IsDeleted == false);
    }

    async Task<Supplier?> ISuppliersService.GetSupplierByIdAsync(Guid supplierId, CancellationToken cancellationToken)
    {
        var result = await suppliersRepository.GetByIdWithBillsAsync(supplierId, cancellationToken);
        return result is null || result.IsDeleted
            ? null
            : result;
    }

    async Task ISuppliersService.AddSupplierAsync(CreateSupplierRequest supplierRequest)
    {
        var supplier = new Supplier { Name = supplierRequest.Name, Address = supplierRequest.Address, };
        await suppliersRepository.AddAsync(supplier);
    }

    async Task<bool> ISuppliersService.UpdateSupplierAsync(
        Guid id, UpdateSupplierRequest supplierRequest, CancellationToken cancellationToken
    )
    {
        var supplier = await suppliersRepository.GetByIdWithBillsAsync(id, cancellationToken);
        if (supplier is null || supplier.IsDeleted)
            return false;

        var newSupplier = new Supplier
        {
            Id = supplier.Id,
            Name = supplierRequest.Name ?? supplier.Name,
            Address = supplierRequest.Address ?? supplier.Address,
        };

        await suppliersRepository.UpdateAsync(newSupplier);
        return true;
    }

    async Task<bool> ISuppliersService.DeleteSupplierAsync(Guid id)
    {
        return await suppliersRepository.DeleteAsync(id);
    }
}
