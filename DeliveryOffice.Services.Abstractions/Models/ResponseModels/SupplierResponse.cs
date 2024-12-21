using DeliveryOffice.Core.Models;

namespace DeliveryOffice.Services.Abstractions.Models.ResponseModels;

/// <summary>
///     Модель ответа для объекта <see cref="Supplier" />
/// </summary>
public class SupplierResponse
{
    /// <summary>
    ///     Идентификатор поставщика
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Имя поставщика
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Адрес поставщика
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    ///     Идентификаторы чеков поставщика
    /// </summary>
    public List<Guid> Bills { get; set; }
}
