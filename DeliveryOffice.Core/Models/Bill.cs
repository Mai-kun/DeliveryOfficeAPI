namespace DeliveryOffice.Core.Models;

/// <summary>
/// Накладная
/// </summary>
public class Bill
{
    public Guid Id { get; set; }

    /// <summary>
    /// Дата накладной
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Склад, где хранятся товары
    /// </summary>
    public required string Warehouse { get; set; }

    /// <summary>
    /// Общая сумма накладной
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Признак оплаты накладной
    /// </summary>
    public bool IsPaid { get; set; }

    /// <summary>
    /// Список товаров в накладной
    /// </summary>
    public List<Product> Products { get; set; } = [];

    /// <summary>
    /// Идентификатор покупателя
    /// </summary>
    public Guid BuyerId { get; set; }

    /// <summary>
    /// Информация о покупателе
    /// </summary>
    public Buyer? Buyer { get; set; }

    /// <summary>
    /// Идентификатор поставщика
    /// </summary>
    public Guid SupplierId { get; set; }

    /// <summary>
    /// Информация о поставщике
    /// </summary>
    public Supplier? Supplier { get; set; }
}
