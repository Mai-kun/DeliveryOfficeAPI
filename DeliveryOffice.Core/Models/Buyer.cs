using DeliveryOffice.Core.Abstractions.EntityRules;

namespace DeliveryOffice.Core.Models;

/// <summary>
///     Покупатель
/// </summary>
public class Buyer : IAuditable, ISoftDelete
{
    /// <summary>
    ///     Идентификатор покупателя
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Имя покупателя
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    ///     Список счетов покупателя
    /// </summary>
    public List<Bill> Bills { get; set; } = [];

    /// <inheritdoc />
    public DateTime CreatedAt { get; set; }

    /// <inheritdoc />
    public DateTime? ModifiedAt { get; set; }

    /// <inheritdoc />
    public bool IsDeleted { get; set; }
}
