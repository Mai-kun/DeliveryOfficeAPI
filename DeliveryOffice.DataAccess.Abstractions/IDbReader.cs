namespace DeliveryOffice.DataAccess.Abstractions;

/// <summary>
///     Контракт на получение данных из БД
/// </summary>
public interface IDbReader
{
    /// <summary>
    ///     Предоставляет функциональные возможности для выполнения запросов
    /// </summary>
    IQueryable<TEntity> Read<TEntity>() where TEntity : class;
}
