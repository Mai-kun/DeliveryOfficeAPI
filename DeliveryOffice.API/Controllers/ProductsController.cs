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
[ApiExplorerSettings(GroupName = $"{nameof(Product)}")]
public class ProductsController : ControllerBase
{
    private readonly IProductsService productsService;
    private readonly IValidatorService validatorService;
    private readonly IMapper mapper;

    public ProductsController(IProductsService productsService, IMapper mapper, IValidatorService validatorService)
    {
        this.productsService = productsService;
        this.mapper = mapper;
        this.validatorService = validatorService;
    }

    /// <summary>
    ///     Получение всех продуктов
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken)
    {
        var products = await productsService.GetAllProductsAsync(cancellationToken);
        var response = mapper.Map<List<ProductResponse>>(products);
        return Ok(response);
    }

    /// <summary>
    ///     Получение продукта по его индентификатору
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductById(Guid id, CancellationToken cancellationToken)
    {
        var product = await productsService.GetProductByIdAsync(id, cancellationToken);
        var response = mapper.Map<ProductResponse>(product);
        return Ok(response);
    }

    /// <summary>
    ///     Добавление нового продукта
    /// </summary>
    [HttpPost]
    public IActionResult AddProduct(CreateProductRequest productRequest, CancellationToken cancellationToken)
    {
        validatorService.Validate(productRequest);
        productsService.AddProduct(productRequest, cancellationToken);
        return Ok();
    }

    /// <summary>
    ///     Обновление данных продукта
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateProduct(
        [FromRoute] Guid id,
        [FromBody] ProductRequest productRequest, CancellationToken cancellationToken
    )
    {
        productRequest.Id = id;
        validatorService.Validate(productRequest);
        await productsService.UpdateProductAsync(productRequest, cancellationToken);
        return Ok();
    }

    /// <summary>
    ///     Удаление продукта
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteProduct(Guid id, CancellationToken cancellationToken)
    {
        await productsService.DeleteProductAsync(id, cancellationToken);
        return Ok();
    }
}
