using Microsoft.AspNetCore.Mvc;

namespace DeliveryOffice.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BillController : ControllerBase
{
    private readonly ILogger<BillController> logger;

    public BillController(ILogger<BillController> logger)
    {
        this.logger = logger;
    }

    // TODO: Write BillController
}
