using DeliveryOffice.Core.Models;

namespace DeliveryOffice.Core.Abstractions.Repositories;

/// <summary>
///     Предоставляет функционал для работы с репозиторием продутов
/// </summary>
public interface IProductRepository
{
    /// <summary>
    ///     Возвращает список всех продуктов
    /// </summary>
    Task<List<Product>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Возвращает продукт по его ID
    /// </summary>
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Добавляет новый продукт
    /// </summary>
    Task AddAsync(Product product);

    /// <summary>
    ///     Обновляет существующий продукт
    /// </summary>
    Task UpdateAsync(Product product);

    /// <summary>
    ///     Удаляет продукт
    /// </summary>
    Task DeleteAsync(Guid id);
}
