using DeliveryOffice.Core.Models;

namespace DeliveryOffice.API.ResponseModels;

/// <summary>
///     Модель ответа для объекта <see cref="Supplier" />
/// </summary>
public class SupplierResponse
{
    /// <inheritdoc cref="Supplier.Id" />
    public Guid Id { get; set; }

    /// <inheritdoc cref="Supplier.Name" />
    public string Name { get; set; }

    /// <inheritdoc cref="Supplier.Address" />
    public string Address { get; set; }

    /// <summary>
    ///     Идентификаторы чеков поставщика
    /// </summary>
    public List<Guid> Bills { get; set; }
}
