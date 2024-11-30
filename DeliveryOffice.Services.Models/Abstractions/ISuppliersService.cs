using DeliveryOffice.Core.Models;
using DeliveryOffice.Services.Models.Models.RequestModels;

namespace DeliveryOffice.Services.Models.Abstractions;

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
    Task AddSupplierAsync(SupplierRequest supplierModel);

    /// <summary>
    ///     Обновляет существующего поставщика
    /// </summary>
    Task UpdateSupplierAsync(Supplier supplier);

    /// <summary>
    ///     Удаляет поставщика
    /// </summary>
    Task DeleteSupplierAsync(Supplier supplier);
}
