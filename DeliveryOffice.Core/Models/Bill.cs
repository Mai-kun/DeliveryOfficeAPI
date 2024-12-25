﻿using DeliveryOffice.Core.Abstractions;

namespace DeliveryOffice.Core.Models;

/// <summary>
///     Накладная
/// </summary>
public class Bill : IEntityWithId, IAuditable, ISoftDelete
{
    /// <inheritdoc />
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
    public int TotalAmount { get; set; }

    /// <summary>
    ///     Признак оплаты накладной
    /// </summary>
    public bool IsPaid { get; set; }

    /// <summary>
    ///     Список товаров в накладной
    /// </summary>
    public List<Product> Products { get; set; } = new List<Product>();

    /// <summary>
    ///     Идентификатор покупателя
    /// </summary>
    public Guid BuyerId { get; set; }

    /// <summary>
    ///     Информация о покупателе
    /// </summary>
    public Buyer? Buyer { get; set; }

    /// <summary>
    ///     Идентификатор поставщика
    /// </summary>
    public Guid SupplierId { get; set; }

    /// <summary>
    ///     Информация о поставщике
    /// </summary>
    public Supplier? Supplier { get; set; }

    /// <summary>
    ///     Дата создания
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    ///     Дата изменения
    /// </summary>
    public DateTimeOffset? ModifiedAt { get; set; }

    /// <inheritdoc />
    public bool IsDeleted { get; set; }
}
