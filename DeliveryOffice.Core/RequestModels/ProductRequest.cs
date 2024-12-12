using System.Text.Json.Serialization;
using DeliveryOffice.Core.Models;

namespace DeliveryOffice.Core.RequestModels;

/// <summary>
///     Модель запроса для объекта <see cref="Product" />
/// </summary>
public class ProductRequest
{
    /// <summary>
    ///     Идентификатор продукта
    /// </summary>
    [JsonIgnore]
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
}
