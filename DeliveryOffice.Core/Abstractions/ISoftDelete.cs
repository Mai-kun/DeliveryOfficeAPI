namespace DeliveryOffice.Core.Abstractions;

/// <summary>
///     Предоставляет контрак по обратимому удалению
/// </summary>
public interface ISoftDelete
{
    /// <summary>
    ///     Флаг удаленного объекта
    /// </summary>
    public bool IsDeleted { get; set; }
}
