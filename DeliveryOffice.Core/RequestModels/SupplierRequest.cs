namespace DeliveryOffice.Core.RequestModels;

/// <summary>
///     Модель запроса для объекта <see cref="Supplier" />
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
    public string? Name { get; set; }

    /// <summary>
    ///     Адрес поставщика
    /// </summary>
    public string? Address { get; set; }
}
