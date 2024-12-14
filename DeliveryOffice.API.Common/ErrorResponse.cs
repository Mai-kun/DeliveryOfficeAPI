namespace DeliveryOffice.API.Common;

/// <summary>
///     Класс для представления ошибки в ответе
/// </summary>
public class ErrorResponse<T>
{
    /// <summary>
    ///     Сообщение(я) об ошибке
    /// </summary>
    public T Message { get; init; }

    /// <summary>
    ///     Код статуса HTTP
    /// </summary>
    public int StatusCode { get; init; }
}
