namespace DeliveryOffice.Services.Abstractions.RequestModels;

/// <summary>
///     Модель запроса для объекта <see cref="ProductRequest" />
/// </summary>
public class ProductRequest
{
    /// <summary>
    ///     Идентификатор продукта
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
}
