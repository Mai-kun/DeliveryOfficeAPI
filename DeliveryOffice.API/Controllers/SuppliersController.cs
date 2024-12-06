using AutoMapper;
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
    private readonly IMapper mapper;

    public SuppliersController(ISuppliersService suppliersService, IMapper mapper)
    {
        this.suppliersService = suppliersService;
        this.mapper = mapper;
    }

    /// <summary>
    ///     Получение всех поставщиков
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAllSuppliers(CancellationToken cancellationToken)
    {
        var suppliers = await suppliersService.GetAllSuppliersAsync(cancellationToken);
        var response = mapper.Map<IEnumerable<SupplierDto>>(suppliers);
        return Ok(response);
    }

    /// <summary>
    ///     Получение поставщика по его индентификатору
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSupplierById(Guid id, CancellationToken cancellationToken)
    {
        var supplier = await suppliersService.GetSupplierByIdAsync(id, cancellationToken);
        if (supplier is null)
        {
            return NotFound("Supplier not found");
        }

        var response = mapper.Map<SupplierDto>(supplier);
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSupplier(
        Guid id, UpdateSupplierRequest supplierRequest, CancellationToken cancellationToken
    )
    {
        var result = await suppliersService.UpdateSupplierAsync(id, supplierRequest, cancellationToken);
        if (result is false)
        {
            return NotFound("Supplier not found");
        }

        return Ok();
    }

    /// <summary>
    ///     Удаление поставщика
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSupplier(Guid id)
    {
        var result = await suppliersService.DeleteSupplierAsync(id);
        if (result is false)
        {
            return NotFound("Supplier not found");
        }

        return Ok();
    }
}
