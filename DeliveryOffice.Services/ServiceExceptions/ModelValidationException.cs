using Microsoft.AspNetCore.Http;

namespace DeliveryOffice.Services.ServiceExceptions;

/// <summary>
///     Ошибка для невалидных моделей
/// </summary>
public class ModelValidationException : Exception
{
    public IEnumerable<KeyValuePair<string, string>> Messages { get; }

    public static int StatusCode => StatusCodes.Status400BadRequest;

    public ModelValidationException(IEnumerable<KeyValuePair<string, string>> messages)
    {
        Messages = messages;
    }
}
