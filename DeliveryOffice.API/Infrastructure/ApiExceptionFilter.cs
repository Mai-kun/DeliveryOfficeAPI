using DeliveryOffice.API.Common;
using DeliveryOffice.Services.ServiceExceptions;
using DeliveryOffice.Services.ServiceExceptions.ForBill;
using DeliveryOffice.Services.ServiceExceptions.ForBuyer;
using DeliveryOffice.Services.ServiceExceptions.ForProduct;
using DeliveryOffice.Services.ServiceExceptions.ForSupplier;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DeliveryOffice.API.Infrastructure;

/// <summary>
///     Фильтр, обрабатывающий ошибки
/// </summary>
public class ApiExceptionFilter : IExceptionFilter
{
    /// <inheritdoc />
    public void OnException(ExceptionContext context)
    {
        context.Result = context.Exception switch
        {
            SupplierException exception => new ObjectResult(
                new ErrorResponse<string> { Message = context.Exception.Message, StatusCode = exception.StatusCode, }),

            ProductException exception => new ObjectResult(
                new ErrorResponse<string> { Message = context.Exception.Message, StatusCode = exception.StatusCode, }),

            BuyerException exception => new ObjectResult(
                new ErrorResponse<string> { Message = context.Exception.Message, StatusCode = exception.StatusCode, }),

            BillException exception => new ObjectResult(
                new ErrorResponse<string> { Message = context.Exception.Message, StatusCode = exception.StatusCode, }),

            ModelValidationException exception => new ObjectResult(
                new ErrorResponse<Dictionary<string, string>>
                {
                    Message = exception.Messages.ToDictionary(k => k.Key, k => k.Value),
                    StatusCode = ModelValidationException.StatusCode,
                }),

            _ => new ObjectResult(
                new ErrorResponse<string>
                {
                    Message = context.Exception.Message, StatusCode = StatusCodes.Status500InternalServerError,
                }),
        };

        context.ExceptionHandled = true;
    }
}
