using Microsoft.AspNetCore.Mvc;

namespace AutoService.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AutoServiceController : ControllerBase
{
    private readonly ILogger<AutoServiceController> _logger;

    public AutoServiceController(ILogger<AutoServiceController> logger)
    {
        _logger = logger;
    }
}