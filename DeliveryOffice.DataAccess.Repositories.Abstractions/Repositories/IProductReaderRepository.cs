using DeliveryOffice.Core.Models;

namespace DeliveryOffice.DataAccess.Repositories.Abstractions.Repositories;

/// <summary>
///     Предоставляет функционал для работы с репозиторием на чтение
/// </summary>
public interface IProductReaderRepository
{
    /// <summary>
    ///     Возвращает список всех продуктов
    /// </summary>
    Task<List<Product>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Возвращает продукт по его ID
    /// </summary>
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
