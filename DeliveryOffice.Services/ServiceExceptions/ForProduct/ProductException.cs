using DeliveryOffice.Core.Models;
using Microsoft.AspNetCore.Http;

namespace DeliveryOffice.Services.ServiceExceptions.ForProduct;

/// <summary>
///     Базовая реализация ошибок для <see cref="Product" />
/// </summary>
public class ProductException : Exception
{
    /// <summary>
    ///     Статус код ошибки
    /// </summary>
    public virtual int StatusCode => StatusCodes.Status400BadRequest;

    protected ProductException(string? message) : base(message)
    {
    }
}
