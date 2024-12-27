using DeliveryOffice.Services.Abstractions.RequestModels.Interfaces;

namespace DeliveryOffice.Services.Abstractions.RequestModels;

/// <summary>
///     Модель запроса для объекта <see cref="SupplierRequest" />
/// </summary>
public class SupplierRequest
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
}
