namespace DeliveryOffice.Services.Abstractions.Models.ResponseModels;

public class BillResponse
{
    /// <summary>
    ///     Идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Дата накладной
    /// </summary>
    public DateTimeOffset Date { get; set; }

    /// <summary>
    ///     Склад, где хранятся товары
    /// </summary>
    public string Warehouse { get; set; }

    /// <summary>
    ///     Общая сумма накладной
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    ///     Признак оплаты накладной
    /// </summary>
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
