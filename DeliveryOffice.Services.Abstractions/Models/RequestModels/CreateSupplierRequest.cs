using DeliveryOffice.Core.Models;

namespace DeliveryOffice.API.Models.RequestModels;

/// <summary>
///     Модель запроса создания для объекта <see cref="Supplier" />
/// </summary>
public class CreateSupplierRequest
{
    /// <summary>
    ///     Имя поставщика
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Адрес поставщика
    /// </summary>
    public string Address { get; set; }
}
