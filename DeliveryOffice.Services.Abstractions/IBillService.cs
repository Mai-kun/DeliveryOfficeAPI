using DeliveryOffice.Core.Models;
using DeliveryOffice.Services.Abstractions.RequestModels;

namespace DeliveryOffice.Services.Abstractions;

/// <summary>
///     Предоставляет функционал для работы с чеками
/// </summary>
public interface IBillService
{
    /// <summary>
    ///     Возвращает список всех чеков
    /// </summary>
    Task<List<Bill>> GetAllBillsAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Возвращает чек по его ID
    /// </summary>
    Task<Bill> GetBillByIdAsync(Guid billId, CancellationToken cancellationToken);

    /// <summary>
    ///     Добавляет новый чек
    /// </summary>
    Task AddBillAsync(CreateBillRequest billRequest, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновляет существующий чек
    /// </summary>
    Task UpdateBillAsync(BillRequest billRequest, CancellationToken cancellationToken);

    /// <summary>
    ///     Удаляет чек
    /// </summary>
    Task DeleteBillAsync(Guid id, CancellationToken cancellationToken);
}
