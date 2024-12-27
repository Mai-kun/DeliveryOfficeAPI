using System.Text.Json.Serialization;
using DeliveryOffice.Services.Abstractions.RequestModels.Interfaces;

namespace DeliveryOffice.Services.Abstractions.RequestModels;

/// <summary>
///     Модель запроса для объекта <see cref="CreateBuyerRequest" />
/// </summary>
public class CreateBuyerRequest
{
    /// <summary>
    /// Идентификатор покупателя
    /// </summary>
    [JsonIgnore]
    public Guid Id { get; set; }

    /// <summary>
    ///     Имя покупателя
    /// </summary>
    public string Name { get; set; }
}
