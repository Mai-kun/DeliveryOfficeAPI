using DeliveryOffice.Core.Models;
using DeliveryOffice.Services.Abstractions.Models.RequestModels;

namespace DeliveryOffice.Services.Abstractions;

/// <summary>
///     Предоставляет функционал для работы с покупателями
/// </summary>
public interface IBuyerService
{
    /// <summary>
    ///     Возвращает список всех <see cref="Buyer"/>
    /// </summary>
    Task<List<Buyer>> GetAllBuyersWithBillsAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Возвращает <see cref="Buyer"/> по его ID
    /// </summary>
    Task<Buyer> GetBuyerByIdWithBillsAsync(Guid productId, CancellationToken cancellationToken);

    /// <summary>
    ///     Добавляет новый <see cref="Buyer"/>
    /// </summary>
    Task AddBuyerAsync(BuyerRequest productRequest, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновляет существующий <see cref="Buyer"/>
    /// </summary>
    Task UpdateBuyerAsync(BuyerRequest buyerRequest, CancellationToken cancellationToken);

    /// <summary>
    ///     Удаляет <see cref="Buyer"/>
    /// </summary>
    Task DeleteBuyerAsync(Guid id, CancellationToken cancellationToken);

}
