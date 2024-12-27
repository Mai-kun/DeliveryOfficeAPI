using DeliveryOffice.Core.Models;
using Microsoft.AspNetCore.Http;

namespace DeliveryOffice.Services.ServiceExceptions.ForSupplier;

/// <summary>
///     Базовая реализация ошибок для <see cref="Supplier" />
/// </summary>
public class SupplierException : Exception
{
    /// <summary>
    ///     Статус код ошибки
    /// </summary>
    public virtual int StatusCode => StatusCodes.Status400BadRequest;

    protected SupplierException(string? message) : base(message)
    {
    }
}
