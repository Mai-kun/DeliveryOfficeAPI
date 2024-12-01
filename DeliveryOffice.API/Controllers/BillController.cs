using DeliveryOffice.Services.Contracts.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryOffice.API.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class BillController : ControllerBase
{
    private readonly ILogger<BillController> logger;
    private readonly ISuppliersService suppliersService;

    public BillController(ILogger<BillController> logger,
        ISuppliersService suppliersService)
    {
        this.logger = logger;
        this.suppliersService = suppliersService;
    }

    [HttpGet]
    public IActionResult GetAllBills()
    {
        return Ok();
    }
    // TODO: Write BillController
}
