using AutoMapper;
using DeliveryOffice.Core.Abstractions.Repositories;
using DeliveryOffice.Core.Abstractions.Services;
using DeliveryOffice.Core.Models;
using DeliveryOffice.Core.RequestModels;
using DeliveryOffice.Services.ServiceExceptions.ForSupplier;

namespace DeliveryOffice.Services;

/// <inheritdoc cref="ISuppliersService" />
public class SuppliersService : ISuppliersService
{
    private readonly ISupplierRepository suppliersRepository;
    private readonly IMapper mapper;

    public SuppliersService(ISupplierRepository suppliersRepository, IMapper mapper)
    {
        this.suppliersRepository = suppliersRepository;
        this.mapper = mapper;
    }

    async Task<List<Supplier>> ISuppliersService.GetAllSuppliersAsync(CancellationToken cancellationToken)
    {
        var result = await suppliersRepository.GetAllWithBillsAsync(cancellationToken);
        var filteredResult = result.Where(s => s.IsDeleted == false).ToList();
        foreach (var supplier in filteredResult)
            supplier.Bills = supplier.Bills
                .Where(b => b.IsDeleted == false)
                .ToList();

        return filteredResult;
    }

    async Task<Supplier> ISuppliersService.GetSupplierByIdAsync(Guid supplierId, CancellationToken cancellationToken)
    {
        var result = await suppliersRepository.GetByIdWithBillsAsync(supplierId, cancellationToken);

        if (result is null || result.IsDeleted)
            throw new SupplierNotFoundException($"Supplier with id: {supplierId} was not found");

        result.Bills = result.Bills.Where(b => b.IsDeleted == false).ToList();
        return result;
    }

    async Task ISuppliersService.AddSupplierAsync(CreateSupplierRequest supplierRequest)
    {
        var supplier = mapper.Map<Supplier>(supplierRequest);
        await suppliersRepository.AddAsync(supplier);
    }

    async Task ISuppliersService.UpdateSupplierAsync(
        SupplierRequest supplierRequest, CancellationToken cancellationToken
    )
    {
        var supplier = await suppliersRepository.GetByIdWithBillsAsync(supplierRequest.Id, cancellationToken);
        if (supplier is null || supplier.IsDeleted)
            throw new SupplierNotFoundException($"Supplier with id: {supplierRequest.Id} was not found");

        var supplierUpdate = mapper.Map<Supplier>(supplierRequest);
        await suppliersRepository.UpdateAsync(supplierUpdate);
    }

    Task ISuppliersService.DeleteSupplierAsync(Guid id)
    {
        return suppliersRepository.DeleteAsync(id);
    }
}
