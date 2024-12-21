using DeliveryOffice.Core.Models;

namespace DeliveryOffice.Services.Abstractions.Models.RequestModels;

/// <summary>
///     Модель запроса создания для объекта <see cref="Product" />
/// </summary>
public class CreateProductRequest
{
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
