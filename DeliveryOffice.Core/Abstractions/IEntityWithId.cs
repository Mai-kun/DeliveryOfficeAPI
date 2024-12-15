namespace DeliveryOffice.Core.Abstractions;

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
