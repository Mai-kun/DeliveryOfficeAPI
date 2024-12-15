using System.Diagnostics.CodeAnalysis;
using DeliveryOffice.API.Common.Abstractions;
using DeliveryOffice.Core.Abstractions;
using DeliveryOffice.DataAccess.Abstractions;

namespace DeliveryOffice.DataAccess.Repositories.Abstractions;

/// <summary>
///     Базовая реализация функционала резоитория для записи в БД
/// </summary>
public abstract class BaseWriteDbRepository<T> : IRepositoryWriter<T> where T : class
{
    private readonly IDbWriter writer;
    private readonly IDateTimeProvider dateTimeProvider;

    protected BaseWriteDbRepository(IDbWriter writer, IDateTimeProvider dateTimeProvider)
    {
        this.writer = writer;
        this.dateTimeProvider = dateTimeProvider;
    }

    void IRepositoryWriter<T>.Add([NotNull] T entity)
    {
        if (entity is IEntityWithId entityWithId && entityWithId.Id == Guid.Empty)
        {
            entityWithId.Id = Guid.NewGuid();
        }

        AuditForCreate(entity);
        writer.Add(entity);
    }

    void IRepositoryWriter<T>.Update([NotNull] T entity)
    {
        AuditForUpdate(entity);
        writer.Update(entity);
    }

    void IRepositoryWriter<T>.Delete([NotNull] T entity)
    {
        if (entity is ISoftDelete)
        {
            AuditForDelete(entity);
            writer.Update(entity);
        }
        else
        {
            writer.Delete(entity);
        }
    }

    private void AuditForCreate(T entity)
    {
        if (entity is IAuditable auditableEntity)
            auditableEntity.CreatedAt = dateTimeProvider.UtcNow;
    }

    private void AuditForUpdate(T entity)
    {
        if (entity is IAuditable auditableEntity)
            auditableEntity.ModifiedAt = dateTimeProvider.UtcNow;
    }

    private void AuditForDelete(T entity)
    {
        if (entity is ISoftDelete softDeleteEntity)
        {
            softDeleteEntity.IsDeleted = true;
        }
    }
}
