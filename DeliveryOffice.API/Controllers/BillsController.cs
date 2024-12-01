using DeliveryOffice.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryOffice.API.Controllers;

[ApiController]
[Route("Api/[controller]")]
[ApiExplorerSettings(GroupName = $"{nameof(Bill)}")]
public class BillsController : ControllerBase
{
    // TODO: Write BillController
}
