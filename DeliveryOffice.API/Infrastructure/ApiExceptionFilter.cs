using DeliveryOffice.Services.ServiceExceptions.ForSupplier;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DeliveryOffice.API.Infrastructure;

public class ApiExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        context.Result = context.Exception switch
        {
            SupplierException exception => new ObjectResult(new ErrorResponse
            {
                Message = context.Exception.Message, StatusCode = exception.StatusCode,
            }),
            _ => new ObjectResult(new ErrorResponse
            {
                Message = "An unknown error occurred.",
                StatusCode = StatusCodes.Status400BadRequest,
            }),
        };

        context.ExceptionHandled = true;
    }
}