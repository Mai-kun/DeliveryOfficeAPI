using DeliveryOffice.Core.Models;
using DeliveryOffice.Core.RequestModels;

namespace DeliveryOffice.Core.Abstractions.Services;

/// <summary>
///     Предоставляет функционал для работы с продуктами
/// </summary>
public interface IProductsService
{
    /// <summary>
    ///     Возвращает список всех продуктов
    /// </summary>
    Task<List<Product>> GetAllProductsAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Возвращает продукт по его ID
    /// </summary>
    Task<Product> GetProductByIdAsync(Guid productId, CancellationToken cancellationToken);

    /// <summary>
    ///     Добавляет новый продукт
    /// </summary>
    Task AddProduct(CreateProductRequest productRequest, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновляет существующий продукт
    /// </summary>
    Task UpdateProductAsync(ProductRequest productRequest, CancellationToken cancellationToken);

    /// <summary>
    ///     Удаляет продукт
    /// </summary>
    Task DeleteProductAsync(Guid id, CancellationToken cancellationToken);
}
