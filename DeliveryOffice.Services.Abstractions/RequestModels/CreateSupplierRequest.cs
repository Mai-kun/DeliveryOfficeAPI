using System.Text.Json.Serialization;

namespace DeliveryOffice.Services.Abstractions.RequestModels;

/// <summary>
///     Модель запроса для объекта <see cref="CreateSupplierRequest" />
/// </summary>
public class CreateSupplierRequest
{
    /// <summary>
    ///     Идентификатор поставщика
    /// </summary>
    [JsonIgnore]
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
