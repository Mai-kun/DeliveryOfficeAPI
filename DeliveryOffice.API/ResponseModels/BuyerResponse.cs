using DeliveryOffice.Core.Models;

namespace DeliveryOffice.API.ResponseModels;

/// <summary>
///     Модель ответа для объекта <see cref="Buyer" />
/// </summary>
public class BuyerResponse
{
    /// <inheritdoc cref="Buyer.Id" />
    public Guid Id { get; set; }

    /// <inheritdoc cref="Buyer.Name" />
    public string Name { get; set; }

    /// <summary>
    ///     Идентификаторы чеков покупателей
    /// </summary>
    public List<Guid> Bills { get; set; }
}
