namespace DeliveryOffice.API.Infrastructure;

/// <summary>
///     Предоставляется функционал валидирования
/// </summary>
public interface IValidatorService
{
    /// <summary>
    ///     Валидириует модель
    /// </summary>
    public void Validate<TModel>(TModel model);
}
