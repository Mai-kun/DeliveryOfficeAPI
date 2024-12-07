namespace DeliveryOffice.API;

/// <summary>
///     Класс для представления ошибки в ответе
/// </summary>
public class ErrorResponse
{
    /// <summary>
    ///     Сообщение об ошибке
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    ///     Код статуса HTTP
    /// </summary>
    public int StatusCode { get; set; }
}