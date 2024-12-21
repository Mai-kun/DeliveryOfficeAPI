using System.Text.Json.Serialization;
using DeliveryOffice.Core.Models;

namespace DeliveryOffice.Services.Abstractions.Models.RequestModels;

/// <summary>
///     Модель запроса для объекта <see cref="Buyer" />
/// </summary>
public class BuyerRequest
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
