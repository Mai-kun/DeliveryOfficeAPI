using DeliveryOffice.Core.Models;
using Microsoft.AspNetCore.Http;

namespace DeliveryOffice.Services.ServiceExceptions.ForBuyer;

/// <summary>
///     Базовая реализация ошибок для <see cref="Buyer" />
/// </summary>
public class BuyerException : Exception
{
    /// <summary>
    ///     Статус код ошибки
    /// </summary>
    public virtual int StatusCode => StatusCodes.Status400BadRequest;

    protected BuyerException(string? message) : base(message)
    {
    }
}
