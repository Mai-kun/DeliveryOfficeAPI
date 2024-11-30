namespace DeliveryOffice.Core.Models;

/// <summary>
/// Товар
/// </summary>
public class Product
{
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование товара
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Количество товара
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Единица измерения товара
    /// </summary>
    public string Unit { get; set; }

    /// <summary>
    /// Цена товара
    /// </summary>
    public decimal Price { get; set; }

    public List<Bill> Bills { get; set; } = [];
}
