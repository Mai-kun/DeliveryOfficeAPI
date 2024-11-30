using DeliveryOffice.Services.Models;
using DeliveryOffice.Services.Models.Abstractions;
using DeliveryOffice.Services.Models.Models.RequestModels;
using DeliveryOffice.Services.Models.Models.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryOffice.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuppliersController : ControllerBase
{
    private readonly ILogger<BillController> logger;
    private readonly ISuppliersService suppliersService;

    public SuppliersController(ILogger<BillController> logger,
        ISuppliersService suppliersService)
    {
        this.logger = logger;
        this.suppliersService = suppliersService;
    }

    [HttpGet]
    public async Task<ActionResult<List<SupplierResponse>>> GetAllSuppliers()
    {
        var suppliers = await suppliersService.GetAllSuppliersAsync();
        var response = suppliers.Select(x => new { x.Id, x.Name, x.Address, });
        return Ok(response);
    }

    // [HttpGet]
    // public async Task<ActionResult<SupplierResponse>> GetSupplierById(int id)
    // {
    //     // TODO: SuppliersController - GetSupplierById;
    //     return null;
    // }

    [HttpPost]
    [Route("Create")]
    public async Task<ActionResult> Add(SupplierRequest supplierRequest)
    {
        await suppliersService.AddSupplierAsync(supplierRequest);
        return Ok();
    }
}
