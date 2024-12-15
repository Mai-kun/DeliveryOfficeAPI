namespace DeliveryOffice.Core.Abstractions;

/// <summary>
///     Предоставляет контракт для ведения аудита
/// </summary>
public interface IAuditable
{
    /// <summary>
    ///     Дата создания
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    ///     Дата изменения
    /// </summary>
    public DateTimeOffset? ModifiedAt { get; set; }
}
