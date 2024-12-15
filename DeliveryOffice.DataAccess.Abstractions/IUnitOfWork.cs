namespace DeliveryOffice.DataAccess.Abstractions;

/// <summary>
///     Определяет функционал для Unit of Work
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    ///     Сохраняет все изменения в БД
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
