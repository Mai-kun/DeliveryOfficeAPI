using AutoMapper;
using DeliveryOffice.Core.Models;
using DeliveryOffice.DataAccess.Abstractions;
using DeliveryOffice.DataAccess.Repositories.Abstractions.Repositories;
using DeliveryOffice.Services.Abstractions;
using DeliveryOffice.Services.Abstractions.RequestModels;
using DeliveryOffice.Services.ServiceExceptions.ForProduct;
using DeliveryOffice.Services.ServiceExceptions.ForSupplier;

namespace DeliveryOffice.Services;

/// <inheritdoc />
public class ProductsService : IProductsService
{
    private readonly IMapper mapper;
    private readonly IProductReaderRepository productReaderRepository;
    private readonly IProductWriterRepository productWriterRepository;
    private readonly IUnitOfWork unitOfWork;

    public ProductsService(
        IMapper mapper, IProductReaderRepository productReaderRepository,
        IProductWriterRepository productWriterRepository, IUnitOfWork unitOfWork
    )
    {
        this.mapper = mapper;
        this.productReaderRepository = productReaderRepository;
        this.productWriterRepository = productWriterRepository;
        this.unitOfWork = unitOfWork;
    }

    Task<List<Product>> IProductsService.GetAllProductsAsync(CancellationToken cancellationToken)
    {
        return productReaderRepository.GetAllAsync(cancellationToken);
    }

    async Task<Product> IProductsService.GetProductByIdAsync(Guid productId, CancellationToken cancellationToken)
    {
        var result = await productReaderRepository.GetByIdAsync(productId, cancellationToken);
        if (result is null)
            throw new ProductNotFoundException($"Product with id: {productId} was not found");

        return result;
    }

    Task IProductsService.AddProduct(CreateProductRequest productRequest, CancellationToken cancellationToken)
    {
        var product = mapper.Map<Product>(productRequest);
        productWriterRepository.Add(product);
        return unitOfWork.SaveChangesAsync(cancellationToken);
    }

    async Task IProductsService.UpdateProductAsync(
        ProductRequest productRequest, CancellationToken cancellationToken
    )
    {
        var product = await productReaderRepository.GetByIdAsync(productRequest.Id, cancellationToken);
        if (product is null)
            throw new ProductNotFoundException($"Product with id: {productRequest.Id} was not found");

        var productUpdate = mapper.Map<Product>(productRequest);
        productWriterRepository.Update(productUpdate);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    async Task IProductsService.DeleteProductAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await productReaderRepository.GetByIdAsync(id, cancellationToken);
        if (product is null)
            throw new ProductNotFoundException($"Product with id: {id} was not found");

        productWriterRepository.Delete(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
