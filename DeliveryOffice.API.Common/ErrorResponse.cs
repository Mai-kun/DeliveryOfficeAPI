namespace DeliveryOffice.API.Common;

/// <summary>
///     Объект ошибки для ответа
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
