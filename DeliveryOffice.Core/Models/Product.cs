using DeliveryOffice.Core.Abstractions.EntityRules;

namespace DeliveryOffice.Core.Models;

/// <summary>
///     Товар
/// </summary>
public class Product : IAuditable, ISoftDelete
{
    public Guid Id { get; set; } = Guid.NewGuid();

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
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <inheritdoc />
    public DateTime? ModifiedAt { get; set; }

    /// <inheritdoc />
    public bool IsDeleted { get; set; }
}
