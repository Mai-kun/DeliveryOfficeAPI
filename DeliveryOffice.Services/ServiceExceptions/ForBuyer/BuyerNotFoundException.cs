using DeliveryOffice.Core.Models;
using Microsoft.AspNetCore.Http;

namespace DeliveryOffice.Services.ServiceExceptions.ForBuyer;

/// <summary>
///     Ошибка отсутствия <see cref="Product" />
/// </summary>
public class BuyerNotFoundException : BuyerException
{
    /// <inheritdoc />
    public override int StatusCode => StatusCodes.Status404NotFound;

    public BuyerNotFoundException(string message) : base(message)
    {
    }
}
