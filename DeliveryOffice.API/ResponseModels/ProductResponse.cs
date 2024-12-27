using DeliveryOffice.Core.Models;

namespace DeliveryOffice.API.ResponseModels;

/// <summary>
///     Модель ответа для объекта <see cref="Product" />
/// </summary>
public class ProductResponse
{
    /// <inheritdoc cref="Product.Id" />
    public Guid Id { get; set; }

    /// <inheritdoc cref="Product.Name" />
    public string Name { get; set; }

    /// <inheritdoc cref="Product.Quantity" />
    public int Quantity { get; set; }

    /// <inheritdoc cref="Product.Unit" />
    public string Unit { get; set; }

    /// <inheritdoc cref="Product.Price" />
    public decimal Price { get; set; }
}
