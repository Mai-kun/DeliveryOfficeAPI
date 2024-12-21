using DeliveryOffice.Core.Models;

namespace DeliveryOffice.DataAccess.Repositories.Abstractions.Repositories;

/// <summary>
///     Предоставляет функционал для работы с репозиторием чтения
/// </summary>
public interface IBillReaderRepository
{
    /// <summary>
    ///     Возвращает список всех поставщиков
    /// </summary>
    Task<List<Bill>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Возвращает поставщика по его ID
    /// </summary>
    Task<Bill?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
