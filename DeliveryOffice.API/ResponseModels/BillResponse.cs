using DeliveryOffice.Core.Models;

namespace DeliveryOffice.API.ResponseModels;

/// <summary>
///     Модель ответа для объекта <see cref="Bill" />
/// </summary>
public class BillResponse
{
    /// <summary>
    ///     Идентификатор накладной
    /// </summary>
    public Guid Id { get; set; }

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
