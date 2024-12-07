using Microsoft.AspNetCore.Http;

namespace DeliveryOffice.Services.ServiceExceptions.ForSupplier;

public class SupplierNotFoundException : SupplierException
{
    public override int StatusCode => StatusCodes.Status404NotFound;

    public SupplierNotFoundException(string message) : base(message)
    {
    }
}