using DeliveryOffice.Core.Models;
using DeliveryOffice.Services.Contracts.Models.RequestModels;

namespace DeliveryOffice.Services.Contracts.Abstractions;

/// <summary>
///     Предоставляет функционал для работы с поставщиками
/// </summary>
public interface ISuppliersService
{
    /// <summary>
    ///     Возвращает список всех поставщиков
    /// </summary>
    Task<List<Supplier>> GetAllSuppliersAsync();

    /// <summary>
    ///     Возвращает поставщика по его ID
    /// </summary>
    Task<Supplier?> GetSupplierByIdAsync(Guid supplierId);

    /// <summary>
    ///     Добавляет нового поставщика
    /// </summary>
    Task AddSupplierAsync(CreateSupplierRequest supplierModel);

    /// <summary>
    ///     Обновляет существующего поставщика
    /// </summary>
    Task<bool> UpdateSupplierAsync(Guid id, UpdateSupplierRequest supplierRequest);

    /// <summary>
    ///     Удаляет поставщика
    /// </summary>
    Task<bool> DeleteSupplierAsync(Guid id);
}
