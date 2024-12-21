using DeliveryOffice.Core.Models;

namespace DeliveryOffice.Services.Abstractions.Models.ResponseModels;

/// <summary>
///     Модель ответа для объекта <see cref="Buyer" />
/// </summary>
public class BuyerResponse
{
    /// <summary>
    ///     Идентификатор покупателя
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Имя покупателя
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Идентификаторы чеков покупателей
    /// </summary>
    public List<Guid> Bills { get; set; }
}
