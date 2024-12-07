using Microsoft.AspNetCore.Http;

namespace DeliveryOffice.Services.ServiceExceptions.ForSupplier;

public abstract class SupplierException : Exception
{
    public virtual int StatusCode => StatusCodes.Status400BadRequest;

    protected SupplierException(string? message) : base(message)
    {
    }
}