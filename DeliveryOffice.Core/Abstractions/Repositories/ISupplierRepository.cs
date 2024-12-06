using DeliveryOffice.Core.Models;

namespace DeliveryOffice.Core.Abstractions.Repositories;

/// <summary>
///     Предоставляет функционал для работы с репозиторием поставщиков
/// </summary>
public interface ISupplierRepository
{
    /// <summary>
    ///     Возвращает список всех поставщиков
    /// </summary>
    Task<List<Supplier>> GetAllWithBillsAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Возвращает поставщика по его ID
    /// </summary>
    Task<Supplier?> GetByIdWithBillsAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Добавляет нового поставщика
    /// </summary>
    Task AddAsync(Supplier supplier);

    /// <summary>
    ///     Обновляет существующего поставщика
    /// </summary>
    Task UpdateAsync(Supplier supplier);

    /// <summary>
    ///     Удаляет поставщика
    /// </summary>
    Task<bool> DeleteAsync(Guid id);
}
