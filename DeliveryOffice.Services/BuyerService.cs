using AutoMapper;
using DeliveryOffice.Core.Models;
using DeliveryOffice.DataAccess.Abstractions;
using DeliveryOffice.DataAccess.Repositories.Abstractions.Repositories;
using DeliveryOffice.Services.Abstractions;
using DeliveryOffice.Services.Abstractions.Models.RequestModels;
using DeliveryOffice.Services.ServiceExceptions.ForBuyer;

namespace DeliveryOffice.Services;

public class BuyerService : IBuyerService
{
    private readonly IMapper mapper;
    private readonly IBuyerReaderRepository buyerReaderRepository;
    private readonly IBuyerWriterRepository buyerWriterRepository;
    private readonly IUnitOfWork unitOfWork;

    public BuyerService(
        IMapper mapper, IBuyerReaderRepository buyerReaderRepository,
        IBuyerWriterRepository buyerWriterRepository, IUnitOfWork unitOfWork
    )
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.buyerReaderRepository = buyerReaderRepository;
        this.buyerWriterRepository = buyerWriterRepository;
    }

    Task<List<Buyer>> IBuyerService.GetAllBuyersWithBillsAsync(CancellationToken cancellationToken)
    {
        return buyerReaderRepository.GetAllWithBillsAsync(cancellationToken);
    }

    async Task<Buyer> IBuyerService.GetBuyerByIdWithBillsAsync(Guid buyerId, CancellationToken cancellationToken)
    {
        var result = await buyerReaderRepository.GetByIdWithBillAsync(buyerId, cancellationToken);
        if (result is null)
            throw new BuyerNotFoundException($"Buyer with id: {buyerId} was not found");

        return result;
    }

    Task IBuyerService.AddBuyer(BuyerRequest buyerRequest, CancellationToken cancellationToken)
    {
        var buyer = mapper.Map<Buyer>(buyerRequest);
        buyerWriterRepository.Add(buyer);
        return unitOfWork.SaveChangesAsync(cancellationToken);
    }

    async Task IBuyerService.UpdateBuyerAsync(BuyerRequest buyerRequest, CancellationToken cancellationToken)
    {
        var buyer = await buyerReaderRepository.GetByIdWithBillAsync(buyerRequest.Id, cancellationToken);
        if (buyer is null)
            throw new BuyerNotFoundException($"Buyer with id: {buyerRequest.Id} was not found");

        var buyerUpdate = mapper.Map<Buyer>(buyerRequest);
        buyerWriterRepository.Update(buyerUpdate);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    async Task IBuyerService.DeleteBuyerAsync(Guid id, CancellationToken cancellationToken)
    {
        var buyer = await buyerReaderRepository.GetByIdWithBillAsync(id, cancellationToken);
        if (buyer is null)
            throw new BuyerNotFoundException($"Buyer with id: {id} was not found");

        buyerWriterRepository.Delete(buyer);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
