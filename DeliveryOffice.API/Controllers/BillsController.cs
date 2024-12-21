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
[ApiExplorerSettings(GroupName = $"{nameof(Bill)}")]
public class BillsController : ControllerBase
{
    private readonly IBillService billService;
    private readonly IValidatorService validatorService;
    private readonly IMapper mapper;

    public BillsController(IBillService billService, IValidatorService validatorService, IMapper mapper)
    {
        this.billService = billService;
        this.validatorService = validatorService;
        this.mapper = mapper;
    }

    /// <summary>
    ///     Получение всех чеков
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllBills(CancellationToken cancellationToken)
    {
        var bills = await billService.GetAllBillsAsync(cancellationToken);
        var response = mapper.Map<List<BillResponse>>(bills);
        return Ok(response);
    }

    /// <summary>
    ///     Получение чека по его индентификатору
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(BillResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBillById(Guid id, CancellationToken cancellationToken)
    {
        var bill = await billService.GetBillByIdAsync(id, cancellationToken);
        var response = mapper.Map<BillResponse>(bill);
        return Ok(response);
    }

    /// <summary>
    ///     Добавление нового чека
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> AddBill(CreateBillRequest billRequest, CancellationToken cancellationToken)
    {
        validatorService.Validate(billRequest);
        await billService.AddBillAsync(billRequest, cancellationToken);
        return Ok();
    }

    /// <summary>
    ///     Обновление данных чека
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateBill(
        [FromRoute] Guid id,
        [FromBody] BillRequest billRequest, CancellationToken cancellationToken
    )
    {
        billRequest.Id = id;
        validatorService.Validate(billRequest);
        await billService.UpdateBillAsync(billRequest, cancellationToken);
        return Ok();
    }

    /// <summary>
    ///     Удаление чека
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteBill(Guid id, CancellationToken cancellationToken)
    {
        await billService.DeleteBillAsync(id, cancellationToken);
        return Ok();
    }
}
