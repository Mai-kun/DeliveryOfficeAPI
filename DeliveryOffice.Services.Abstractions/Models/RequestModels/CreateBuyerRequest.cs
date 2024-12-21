using DeliveryOffice.Core.Models;

namespace DeliveryOffice.Services.Abstractions.Models.RequestModels;

/// <summary>
///     Модель запроса создания для объекта <see cref="Buyer" />
/// </summary>
public class CreateBuyerRequest
{
    /// <summary>
    ///     Имя покупателя
    /// </summary>
    public string Name { get; set; }
}
