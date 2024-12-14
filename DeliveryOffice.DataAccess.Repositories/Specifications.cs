using DeliveryOffice.Core.Abstractions.EntityRules;

namespace DeliveryOffice.DataAccess.Repositories;

public static class Specifications
{
    public static IQueryable<TEntity> ById<TEntity>(this IQueryable<TEntity> query, Guid id)
        where TEntity : class, IEntityWithId
    {
        return query.Where(x => x.Id == id);
    }

    public static IQueryable<TEntity> NotDeleted<TEntity>(this IQueryable<TEntity> query)
        where TEntity : class, ISoftDelete
    {
        return query.Where(x => x.IsDeleted == false);
    }
}
