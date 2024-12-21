using AutoMapper;
using DeliveryOffice.Core.Models;
using DeliveryOffice.DataAccess.Abstractions;
using DeliveryOffice.DataAccess.Repositories.Abstractions.Repositories;
using DeliveryOffice.Services.Abstractions;
using DeliveryOffice.Services.Abstractions.Models.RequestModels;
using DeliveryOffice.Services.ServiceExceptions.ForBill;
using DeliveryOffice.Services.ServiceExceptions.ForBuyer;

namespace DeliveryOffice.Services;

/// <inheritdoc />
public class BillService : IBillService
{
    private readonly IMapper mapper;
    private readonly IBillReaderRepository billReaderRepository;
    private readonly IBillWriterRepository billWriterRepository;
    private readonly IUnitOfWork unitOfWork;

    public BillService(
        IMapper mapper, IBillReaderRepository billReaderRepository, IBillWriterRepository billWriterRepository,
        IUnitOfWork unitOfWork
    )
    {
        this.mapper = mapper;
        this.billReaderRepository = billReaderRepository;
        this.billWriterRepository = billWriterRepository;
        this.unitOfWork = unitOfWork;
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

    Task IBillService.AddBill(CreateBillRequest billRequest, CancellationToken cancellationToken)
    {
        var bill = mapper.Map<Bill>(billRequest);
        billWriterRepository.Add(bill);
        return unitOfWork.SaveChangesAsync(cancellationToken);
    }

    async Task IBillService.UpdateBillAsync(BillRequest billRequest, CancellationToken cancellationToken)
    {
        var bill = await billReaderRepository.GetByIdAsync(billRequest.Id, cancellationToken);
        if (bill is null)
            throw new BuyerNotFoundException($"Bill with id: {billRequest.Id} was not found");

        var billUpdate = mapper.Map<Bill>(billRequest);
        billWriterRepository.Update(billUpdate);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    async Task IBillService.DeleteBillAsync(Guid id, CancellationToken cancellationToken)
    {
        var bill = await billReaderRepository.GetByIdAsync(id, cancellationToken);
        if (bill is null)
            throw new BuyerNotFoundException($"Bill with id: {id} was not found");

        billWriterRepository.Delete(bill);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
