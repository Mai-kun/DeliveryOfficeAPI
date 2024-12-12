using DeliveryOffice.Core.Models;
using Microsoft.AspNetCore.Http;

namespace DeliveryOffice.Services.ServiceExceptions.ForProduct;

/// <summary>
///     Ошибка отсутствия <see cref="Product" />
/// </summary>
public class ProductNotFoundException : ProductException
{
    public override int StatusCode => StatusCodes.Status404NotFound;

    public ProductNotFoundException(string message) : base(message)
    {
    }
}
