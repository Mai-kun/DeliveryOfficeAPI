namespace DeliveryOffice.Core.Abstractions.EntityRules;

/// <summary>
///     Предоставляет контракт для моделей с индентиыикатором
/// </summary>
public interface IEntityWithId
{
    /// <summary>
    ///     Индектификатор
    /// </summary>
    public Guid Id { get; set; }
}
