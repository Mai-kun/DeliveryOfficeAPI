using DeliveryOffice.Core.Models;
using Microsoft.AspNetCore.Http;

namespace DeliveryOffice.Services.ServiceExceptions.ForBill;

/// <summary>
///     Базовая реализация ошибок для <see cref="Bill" />
/// </summary>
public class BillException : Exception
{
    /// <summary>
    ///     Статус код ошибки
    /// </summary>
    public virtual int StatusCode => StatusCodes.Status400BadRequest;

    protected BillException(string? message) : base(message)
    {
    }
}
