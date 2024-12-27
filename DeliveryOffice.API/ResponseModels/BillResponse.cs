using DeliveryOffice.Core.Models;

namespace DeliveryOffice.API.ResponseModels;

/// <summary>
///     Модель ответа для объекта <see cref="Bill" />
/// </summary>
public class BillResponse
{
    /// <inheritdoc cref="Bill.Id" />
    public Guid Id { get; set; }

    /// <inheritdoc cref="Bill.Date" />
    public DateTimeOffset Date { get; set; }

    /// <inheritdoc cref="Bill.Warehouse" />
    public string Warehouse { get; set; }

    /// <inheritdoc cref="Bill.TotalAmount" />
    public decimal TotalAmount { get; set; }

    /// <inheritdoc cref="Bill.IsPaid" />
    public bool IsPaid { get; set; }

    /// <summary>
    ///     Названия товаров в накладной
    /// </summary>
    public List<string> ProductNames { get; set; }

    /// <summary>
    ///     Имя покупателя
    /// </summary>
    public string BuyerName { get; set; }

    /// <summary>
    ///     Имя поставщика
    /// </summary>
    public string SupplierName { get; set; }
}
