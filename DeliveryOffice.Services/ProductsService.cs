using AutoMapper;
using DeliveryOffice.Core.Abstractions.Repositories;
using DeliveryOffice.Core.Abstractions.Services;
using DeliveryOffice.Core.Models;
using DeliveryOffice.Core.RequestModels;
using DeliveryOffice.Services.ServiceExceptions.ForProduct;
using DeliveryOffice.Services.ServiceExceptions.ForSupplier;

namespace DeliveryOffice.Services;

public class ProductsService : IProductsService
{
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;

    public ProductsService(IProductRepository productRepository, IMapper mapper)
    {
        this.productRepository = productRepository;
        this.mapper = mapper;
    }

    async Task<List<Product>> IProductsService.GetAllProductsAsync(CancellationToken cancellationToken)
    {
        var result = await productRepository.GetAllAsync(cancellationToken);
        return result
               .Where(p => p.IsDeleted == false)
               .ToList();
    }

    async Task<Product> IProductsService.GetProductByIdAsync(Guid productId, CancellationToken cancellationToken)
    {
        var result = await productRepository.GetByIdAsync(productId, cancellationToken);
        if (result is null || result.IsDeleted)
            throw new ProductNotFoundException($"Product with id: {productId} was not found");

        return result;
    }

    async Task IProductsService.AddProductAsync(CreateProductRequest productRequest)
    {
        var product = mapper.Map<Product>(productRequest);
        await productRepository.AddAsync(product);
    }

    async Task IProductsService.UpdateProductAsync(
        ProductRequest productRequest, CancellationToken cancellationToken
    )
    {
        var product = await productRepository.GetByIdAsync(productRequest.Id, cancellationToken);
        if (product is null || product.IsDeleted)
            throw new SupplierNotFoundException($"Product with id: {productRequest.Id} was not found");

        var productUpdate = mapper.Map<Product>(productRequest);
        await productRepository.UpdateAsync(productUpdate);
    }

    Task IProductsService.DeleteProductAsync(Guid id)
    {
        return productRepository.DeleteAsync(id);
    }
}
