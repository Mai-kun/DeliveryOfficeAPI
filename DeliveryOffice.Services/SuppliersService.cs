using AutoMapper;
using DeliveryOffice.Core.Models;
using DeliveryOffice.DataAccess.Abstractions;
using DeliveryOffice.DataAccess.Repositories.Abstractions.Repositories;
using DeliveryOffice.Services.Abstractions;
using DeliveryOffice.Services.Abstractions.RequestModels;
using DeliveryOffice.Services.ServiceExceptions.ForSupplier;

namespace DeliveryOffice.Services;

/// <inheritdoc cref="ISuppliersService" />
public class SuppliersService : ISuppliersService
{
    private readonly IMapper mapper;
    private readonly ISupplierReaderRepository supplierReaderRepository;
    private readonly ISupplierWriterRepository supplierWriterRepository;
    private readonly IUnitOfWork unitOfWork;

    public SuppliersService(
        IMapper mapper, ISupplierReaderRepository supplierReaderRepository,
        ISupplierWriterRepository supplierWriterRepository, IUnitOfWork unitOfWork
    )
    {
        this.mapper = mapper;
        this.supplierReaderRepository = supplierReaderRepository;
        this.supplierWriterRepository = supplierWriterRepository;
        this.unitOfWork = unitOfWork;
    }

    async Task<List<Supplier>> ISuppliersService.GetAllSuppliersAsync(CancellationToken cancellationToken)
    {
        return await supplierReaderRepository.GetAllWithBillsAsync(cancellationToken);
    }

    async Task<Supplier> ISuppliersService.GetSupplierByIdAsync(Guid supplierId, CancellationToken cancellationToken)
    {
        var result = await supplierReaderRepository.GetByIdWithBillsAsync(supplierId, cancellationToken);
        if (result is null)
            throw new SupplierNotFoundException($"Supplier with id: {supplierId} was not found");

        return result;
    }

    Task ISuppliersService.AddSupplier(
        CreateSupplierRequest supplierRequest, CancellationToken cancellationToken
    )
    {
        var supplier = mapper.Map<Supplier>(supplierRequest);
        supplierWriterRepository.Add(supplier);
        return unitOfWork.SaveChangesAsync(cancellationToken);
    }

    async Task ISuppliersService.UpdateSupplierAsync(
        SupplierRequest supplierRequest, CancellationToken cancellationToken
    )
    {
        var supplier = await supplierReaderRepository.GetByIdWithBillsAsync(supplierRequest.Id, cancellationToken);
        if (supplier is null)
            throw new SupplierNotFoundException($"Supplier with id: {supplierRequest.Id} was not found");

        var supplierUpdate = mapper.Map<Supplier>(supplierRequest);
        supplierWriterRepository.Update(supplierUpdate);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    async Task ISuppliersService.DeleteSupplierAsync(Guid id, CancellationToken cancellationToken)
    {
        var supplier = await supplierReaderRepository.GetByIdWithBillsAsync(id, cancellationToken);
        if (supplier is null)
            throw new SupplierNotFoundException($"Supplier with id: {id} was not found");

        supplierWriterRepository.Delete(supplier);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
