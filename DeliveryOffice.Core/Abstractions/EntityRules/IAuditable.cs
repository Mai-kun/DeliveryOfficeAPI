namespace DeliveryOffice.Core.Abstractions.EntityRules;

/// <summary>
///     Предоставляет контракт для ведения аудита
/// </summary>
public interface IAuditable
{
    /// <summary>
    ///     Дата создания
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    ///     Дата изменения
    /// </summary>
    public DateTime? ModifiedAt { get; set; }
}
