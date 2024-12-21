using DeliveryOffice.Core.Models;
using Microsoft.AspNetCore.Http;

namespace DeliveryOffice.Services.ServiceExceptions.ForBill;

/// <summary>
///     Ошибка отсутствия <see cref="Bill" />
/// </summary>
public class BillNotFoundException : BillException
{
    public override int StatusCode => StatusCodes.Status404NotFound;

    public BillNotFoundException(string message) : base(message)
    {
    }
}
