using DeliveryOffice.Core.Models;
using Microsoft.AspNetCore.Http;

namespace DeliveryOffice.Services.ServiceExceptions.ForSupplier;

/// <summary>
///     Ошибка отсутствия <see cref="Supplier" />
/// </summary>
public class SupplierNotFoundException : SupplierException
{
    /// <inheritdoc />
    public override int StatusCode => StatusCodes.Status404NotFound;

    public SupplierNotFoundException(string message) : base(message)
    {
    }
}
