namespace DeliveryOffice.Core.Models;

/// <summary>
/// Покупатель
/// </summary>
public class Buyer
{
    /// <summary>
    /// Идентификатор покупателя
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Имя покупателя
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Список счетов покупателя
    /// </summary>
    public List<Bill> Bills { get; set; } = [];
}
