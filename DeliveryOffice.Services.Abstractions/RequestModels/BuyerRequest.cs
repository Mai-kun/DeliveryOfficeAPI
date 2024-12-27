using System.Text.Json.Serialization;
using DeliveryOffice.Services.Abstractions.RequestModels.Interfaces;

namespace DeliveryOffice.Services.Abstractions.RequestModels;

/// <summary>
///     Модель запроса для объекта <see cref="BuyerRequest" />
/// </summary>
public class BuyerRequest
{
    /// <summary>
    ///     Идентификатор покупателя
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Имя покупателя
    /// </summary>
    public string Name { get; set; }
}
