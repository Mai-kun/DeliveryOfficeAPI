using DeliveryOffice.Core.Models;

namespace DeliveryOffice.Services.Abstractions.Models.RequestModels;

/// <summary>
///     Модель запроса для объекта <see cref="Bill" />
/// </summary>
public class BillRequest
{
    /// <summary>
    /// Идентификатор накладной
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
}
