using DeliveryOffice.Core.Abstractions;

namespace DeliveryOffice.Core.Models;

/// <summary>
///     Товар
/// </summary>
public class Product : IEntityWithId, IAuditable, ISoftDelete
{
    /// <summary>
    ///     Индектификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Наименование товара
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Количество товара
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    ///     Единица измерения товара
    /// </summary>
    public string Unit { get; set; }

    /// <summary>
    ///     Цена товара
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    ///     Список счетов
    /// </summary>
    public List<Bill> Bills { get; set; } = new();

    /// <inheritdoc />
    public DateTimeOffset CreatedAt { get; set; }

    /// <inheritdoc />
    public DateTimeOffset? ModifiedAt { get; set; }

    /// <inheritdoc />
    public bool IsDeleted { get; set; }
}
