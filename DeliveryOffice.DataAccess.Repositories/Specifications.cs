using DeliveryOffice.Core.Abstractions;

namespace DeliveryOffice.DataAccess.Repositories;

/// <summary>
///     Правила для получения данных
/// </summary>
public static class Specifications
{
    /// <summary>
    ///     По идентификатору
    /// </summary>
    public static IQueryable<TEntity> ById<TEntity>(this IQueryable<TEntity> query, Guid id)
        where TEntity : class, IEntityWithId
    {
        return query.Where(x => x.Id == id);
    }

    /// <summary>
    ///     Не удаленные записи
    /// </summary>
    public static IQueryable<TEntity> NotDeleted<TEntity>(this IQueryable<TEntity> query)
        where TEntity : class, ISoftDelete
    {
        return query.Where(x => x.IsDeleted == false);
    }
}
