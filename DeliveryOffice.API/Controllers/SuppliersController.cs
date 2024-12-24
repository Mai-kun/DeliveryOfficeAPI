using AutoMapper;
using DeliveryOffice.API.Infrastructure;
using DeliveryOffice.Core.Models;
using DeliveryOffice.Services.Abstractions;
using DeliveryOffice.Services.Abstractions.Models.RequestModels;
using DeliveryOffice.Services.Abstractions.Models.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryOffice.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiExplorerSettings(GroupName = $"{nameof(Supplier)}")]
public class SuppliersController : ControllerBase
{
    private readonly ISuppliersService suppliersService;
    private readonly IValidatorService validatorService;
    private readonly IMapper mapper;

    public SuppliersController(ISuppliersService suppliersService, IMapper mapper, IValidatorService validatorService)
    {
        this.suppliersService = suppliersService;
        this.mapper = mapper;
        this.validatorService = validatorService;
    }

    /// <summary>
    ///     Получение всех поставщиков
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllSuppliers(CancellationToken cancellationToken)
    {
        var suppliers = await suppliersService.GetAllSuppliersAsync(cancellationToken);
        var result = mapper.Map<List<SupplierResponse>>(suppliers);
        return Ok(result);
    }

    /// <summary>
    ///     Получение поставщика по его индентификатору
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SupplierResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSupplierById(Guid id, CancellationToken cancellationToken)
    {
        var supplier = await suppliersService.GetSupplierByIdAsync(id, cancellationToken);
        var response = mapper.Map<SupplierResponse>(supplier);
        return Ok(response);
    }

    /// <summary>
    ///     Добавление нового поставщика
    /// </summary>
    [HttpPost]
    public IActionResult AddSupplier(SupplierRequest supplierRequest, CancellationToken cancellationToken)
    {
        validatorService.Validate(supplierRequest);
        suppliersService.AddSupplier(supplierRequest, cancellationToken);
        return Ok();
    }

    /// <summary>
    ///     Обновление данных поставщика
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateSupplier(
        [FromRoute] Guid id,
        [FromBody] SupplierRequest supplierRequest, CancellationToken cancellationToken
    )
    {
        supplierRequest.Id = id;
        validatorService.Validate(supplierRequest);
        await suppliersService.UpdateSupplierAsync(supplierRequest, cancellationToken);
        return Ok();
    }

    /// <summary>
    ///     Удаление поставщика
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteSupplier(Guid id, CancellationToken cancellationToken)
    {
        await suppliersService.DeleteSupplierAsync(id, cancellationToken);
        return Ok();
    }
}
