using DeliveryOffice.Core.Models;

namespace DeliveryOffice.API.Models.ResponseModels;

/// <summary>
///     Модель ответа для объекта <see cref="Product" />
/// </summary>
public class ProductResponse
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
