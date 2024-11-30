namespace DeliveryOffice.Core.Models;

/// <summary>
/// Поставщик
/// </summary>
public class Supplier
{
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование поставщика (компании)
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Адрес поставщика
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Список счетов поставщика
    /// </summary>
    public List<Bill> Bills { get; set; } = [];
}
