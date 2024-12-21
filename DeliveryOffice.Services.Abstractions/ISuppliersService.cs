using DeliveryOffice.Core.Models;
using DeliveryOffice.Services.Abstractions.Models.RequestModels;

namespace DeliveryOffice.Services.Abstractions;

/// <summary>
///     Предоставляет функционал для работы с поставщиками
/// </summary>
public interface ISuppliersService
{
    /// <summary>
    ///     Возвращает список всех поставщиков
    /// </summary>
    Task<List<Supplier>> GetAllSuppliersAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Возвращает поставщика по его ID
    /// </summary>
    Task<Supplier> GetSupplierByIdAsync(Guid supplierId, CancellationToken cancellationToken);

    /// <summary>
    ///     Добавляет нового поставщика
    /// </summary>
    Task AddSupplier(CreateSupplierRequest supplierModel, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновляет существующего поставщика
    /// </summary>
    Task UpdateSupplierAsync(SupplierRequest supplierRequest, CancellationToken cancellationToken);

    /// <summary>
    ///     Удаляет поставщика
    /// </summary>
    Task DeleteSupplierAsync(Guid id, CancellationToken cancellationToken);
}
