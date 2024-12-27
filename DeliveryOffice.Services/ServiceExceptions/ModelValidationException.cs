using Microsoft.AspNetCore.Http;

namespace DeliveryOffice.Services.ServiceExceptions;

/// <summary>
///     Модель ошибок для невалидных моделей
/// </summary>
public class ModelValidationException : Exception
{
    /// <summary>
    ///     Словарь ошибок
    /// </summary>
    public IEnumerable<KeyValuePair<string, string>> Messages { get; }

    /// <summary>
    ///     Статус код ошибки
    /// </summary>
    public static int StatusCode => StatusCodes.Status400BadRequest;

    public ModelValidationException(IEnumerable<KeyValuePair<string, string>> messages)
    {
        Messages = messages;
    }
}
