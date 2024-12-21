using DeliveryOffice.Core.Models;

namespace DeliveryOffice.DataAccess.Repositories.Abstractions.Repositories;

/// <summary>
///     Предоставляет функционал для работы с репозиторием чтения
/// </summary>
public interface ISupplierReaderRepository
{
    /// <summary>
    ///     Возвращает список всех поставщиков
    /// </summary>
    Task<List<Supplier>> GetAllWithBillsAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Возвращает поставщика по его ID вместе с его чеками
    /// </summary>
    Task<Supplier?> GetByIdWithBillsAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Возвращает поставщика по его ID
    /// </summary>
    Task<Supplier?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
