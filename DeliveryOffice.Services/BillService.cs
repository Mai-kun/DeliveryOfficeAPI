using AutoMapper;
using DeliveryOffice.Core.Models;
using DeliveryOffice.DataAccess.Abstractions;
using DeliveryOffice.DataAccess.Repositories.Abstractions.Repositories;
using DeliveryOffice.Services.Abstractions;
using DeliveryOffice.Services.Abstractions.Models.RequestModels;
using DeliveryOffice.Services.ServiceExceptions.ForBill;
using DeliveryOffice.Services.ServiceExceptions.ForBuyer;
using DeliveryOffice.Services.ServiceExceptions.ForProduct;
using DeliveryOffice.Services.ServiceExceptions.ForSupplier;

namespace DeliveryOffice.Services;

/// <inheritdoc />
public class BillService : IBillService
{
    private readonly IBillReaderRepository billReaderRepository;
    private readonly IBillWriterRepository billWriterRepository;
    private readonly ISupplierReaderRepository supplierReaderRepository;
    private readonly IBuyerReaderRepository buyerReaderRepository;
    private readonly IProductReaderRepository productReaderRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public BillService(
        IMapper mapper, IBillReaderRepository billReaderRepository, IBillWriterRepository billWriterRepository,
        IUnitOfWork unitOfWork, ISupplierReaderRepository supplierReaderRepository,
        IBuyerReaderRepository buyerReaderRepository, IProductReaderRepository productReaderRepository
    )
    {
        this.mapper = mapper;
        this.billReaderRepository = billReaderRepository;
        this.billWriterRepository = billWriterRepository;
        this.unitOfWork = unitOfWork;
        this.supplierReaderRepository = supplierReaderRepository;
        this.buyerReaderRepository = buyerReaderRepository;
        this.productReaderRepository = productReaderRepository;
    }

    Task<List<Bill>> IBillService.GetAllBillsAsync(CancellationToken cancellationToken)
    {
        return billReaderRepository.GetAllAsync(cancellationToken);
    }

    async Task<Bill> IBillService.GetBillByIdAsync(Guid billId, CancellationToken cancellationToken)
    {
        var result = await billReaderRepository.GetByIdAsync(billId, cancellationToken);
        if (result is null)
            throw new BillNotFoundException($"Bill with id: {billId} was not found");

        return result;
    }

    async Task IBillService.AddBillAsync(CreateBillRequest billRequest, CancellationToken cancellationToken)
    {
        var buyer = await buyerReaderRepository.GetByIdAsync(billRequest.BuyerId, cancellationToken);
        if (buyer is null)
            throw new BuyerNotFoundException($"Buyer with id: {billRequest.BuyerId} was not found");

        var supplier = await supplierReaderRepository.GetByIdAsync(billRequest.SupplierId, cancellationToken);
        if (supplier is null)
            throw new SupplierNotFoundException($"Supplier with id: {billRequest.SupplierId} was not found");

        var bill = mapper.Map<Bill>(billRequest);
        billWriterRepository.Add(bill);

        var products = await productReaderRepository.GetAllAsync(cancellationToken);
        var filteredProducts = products
                               .Where(p => billRequest.Products.Contains(p.Id))
                               .ToList();

        var missingProductIds = billRequest.Products.Except(filteredProducts.Select(p => p.Id)).ToList();
        if (missingProductIds.Any())
            throw new ProductNotFoundException(
                $"Products with ids {string.Join(", ", missingProductIds)} were not found");

        foreach (var product in filteredProducts)
        {
            unitOfWork.Attach(product);
            bill.Products.Add(product);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    async Task IBillService.UpdateBillAsync(BillRequest billRequest, CancellationToken cancellationToken)
    {
        var bill = await billReaderRepository.GetByIdAsync(billRequest.Id, cancellationToken);
        if (bill is null)
            throw new BillNotFoundException($"Bill with id: {billRequest.Id} was not found");

        mapper.Map(billRequest, bill);
        billWriterRepository.Update(bill);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    async Task IBillService.DeleteBillAsync(Guid id, CancellationToken cancellationToken)
    {
        var bill = await billReaderRepository.GetByIdAsync(id, cancellationToken);
        if (bill is null)
            throw new BillNotFoundException($"Bill with id: {id} was not found");

        billWriterRepository.Delete(bill);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
