using System.Text.Json.Serialization;

namespace DeliveryOffice.Services.Abstractions.RequestModels;

/// <summary>
///     Модель запроса для объекта <see cref="CreateProductRequest" />
/// </summary>
public class CreateProductRequest
{
    /// <summary>
    ///     Идентификатор продукта
    /// </summary>
    [JsonIgnore]
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
}
