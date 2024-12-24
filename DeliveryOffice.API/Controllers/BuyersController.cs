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
[ApiExplorerSettings(GroupName = $"{nameof(Buyer)}")]
public class BuyersController : ControllerBase
{
    private readonly IBuyerService buyerService;
    private readonly IValidatorService validatorService;
    private readonly IMapper mapper;

    public BuyersController(IBuyerService buyerService, IValidatorService validatorService, IMapper mapper)
    {
        this.buyerService = buyerService;
        this.validatorService = validatorService;
        this.mapper = mapper;
    }

    /// <summary>
    ///     Получение всех покупателей
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllBuyers(CancellationToken cancellationToken)
    {
        var buyers = await buyerService.GetAllBuyersWithBillsAsync(cancellationToken);
        var response = mapper.Map<List<BuyerResponse>>(buyers);
        return Ok(response);
    }

    /// <summary>
    ///     Получение покупателя по его индентификатору
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(BuyerResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBuyerById(Guid id, CancellationToken cancellationToken)
    {
        var buyer = await buyerService.GetBuyerByIdWithBillsAsync(id, cancellationToken);
        var response = mapper.Map<BuyerResponse>(buyer);
        return Ok(response);
    }

    /// <summary>
    ///     Добавление нового покупателя
    /// </summary>
    [HttpPost]
    public IActionResult AddBuyer(BuyerRequest buyerRequest, CancellationToken cancellationToken)
    {
        validatorService.Validate(buyerRequest);
        buyerService.AddBuyer(buyerRequest, cancellationToken);
        return Ok();
    }

    /// <summary>
    ///     Обновление данных покупателя
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateBuyer(
        [FromRoute] Guid id,
        [FromBody] BuyerRequest buyerRequest, CancellationToken cancellationToken
    )
    {
        buyerRequest.Id = id;
        validatorService.Validate(buyerRequest);
        await buyerService.UpdateBuyerAsync(buyerRequest, cancellationToken);
        return Ok();
    }

    /// <summary>
    ///     Удаление покупателя
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteBuyer(Guid id, CancellationToken cancellationToken)
    {
        await buyerService.DeleteBuyerAsync(id, cancellationToken);
        return Ok();
    }
}
