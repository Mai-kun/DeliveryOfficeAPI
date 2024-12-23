﻿using DeliveryOffice.Core.Models;

namespace DeliveryOffice.Services.Abstractions.Models.RequestModels;

/// <summary>
///     Модель запроса создания для объекта <see cref="Bill" />
/// </summary>
public class CreateBillRequest
{
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
    ///     Идентификаторы товаров
    /// </summary>
    public List<Guid> Products { get; set; }

    /// <summary>
    ///     Идентификатор покупателя
    /// </summary>
    public Guid BuyerId { get; set; }

    /// <summary>
    ///     Идентификатор поставщика
    /// </summary>
    public Guid SupplierId { get; set; }
}
