using DeliveryOffice.Core.Models;

namespace DeliveryOffice.DataAccess.Repositories.Abstractions.Repositories;

/// <summary>
///     Предоставляет функционал для работы с репозиторием на чтение
/// </summary>
public interface IBuyerReaderRepository
{
    /// <summary>
    ///     Возвращает список всех покупателей
    /// </summary>
    Task<List<Buyer>> GetAllWithBillsAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Возвращает покупателя по его ID вместе с его чеками
    /// </summary>
    Task<Buyer?> GetByIdWithBillAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Возвращает покупателя по его ID
    /// </summary>
    Task<Buyer?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
