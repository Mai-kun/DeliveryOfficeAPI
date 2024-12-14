namespace DeliveryOffice.DataAccess.Abstractions;

/// <summary>
///     Контракт на запись данных в БД
/// </summary>
public interface IDbWriter
{
    /// <summary>
    ///     Добавить новую запись
    /// </summary>
    void Add<TEntity>(TEntity entity) where TEntity : class;

    /// <summary>
    ///     Изменить запись
    /// </summary>
    void Update<TEntity>(TEntity entity) where TEntity : class;

    /// <summary>
    ///     Удалить запись
    /// </summary>
    void Delete<TEntity>(TEntity entity) where TEntity : class;
}
