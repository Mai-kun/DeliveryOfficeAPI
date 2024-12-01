using DeliveryOffice.Core.Models;
using DeliveryOffice.Services.Contracts.Abstractions;
using DeliveryOffice.Services.Contracts.Models.RequestModels;
using DeliveryOffice.Services.Contracts.Models.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryOffice.API.Controllers;

[ApiController]
[Route("Api/[controller]")]
[ApiExplorerSettings(GroupName = $"{nameof(Supplier)}")]
public class SuppliersController : ControllerBase
{
    private readonly ISuppliersService suppliersService;

    public SuppliersController(ISuppliersService suppliersService)
    {
        this.suppliersService = suppliersService;
    }

    /// <summary>
    ///     Получение всех поставщиков
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAllSuppliers()
    {
        var suppliers = await suppliersService.GetAllSuppliersAsync();
        var response = suppliers.Select(x => new SupplierResponse(x.Id, x.Name, x.Address));
        return Ok(response);
    }

    /// <summary>
    ///     Получение поставщика по его индентивикатору
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetSupplierById(Guid id)
    {
        var supplier = await suppliersService.GetSupplierByIdAsync(id);
        if (supplier is null)
        {
            return NoContent();
        }

        var response = new SupplierResponse(supplier.Id, supplier.Name, supplier.Address);
        return Ok(response);
    }

    /// <summary>
    ///     Добавление нового поставщика
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> AddSupplier(CreateSupplierRequest supplierRequest)
    {
        await suppliersService.AddSupplierAsync(supplierRequest);
        return Ok();
    }

    /// <summary>
    ///     Обновление данных существующего поставщика
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSupplier(Guid id, UpdateSupplierRequest supplierRequest)
    {
        var result = await suppliersService.UpdateSupplierAsync(id, supplierRequest);
        if (result is false)
        {
            return NoContent();
        }

        return Ok();
    }

    /// <summary>
    ///     Удаление поставщика
    /// </summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteSupplier(Guid id)
    {
        var result = await suppliersService.DeleteSupplierAsync(id);
        if (result is false)
        {
            return NoContent();
        }

        return Ok();
    }
}
