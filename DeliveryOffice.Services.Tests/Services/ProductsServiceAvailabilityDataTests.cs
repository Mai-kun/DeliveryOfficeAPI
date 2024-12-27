using Ahatornn.TestGenerator;
using DeliveryOffice.Core.Models;
using DeliveryOffice.DataAccess.Tests;
using DeliveryOffice.Services.Abstractions.RequestModels;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Priority;

namespace DeliveryOffice.Services.Tests.Services;

/// <summary>
///     Тесты для <see cref="ProductsService" /> при работе с данными
/// </summary>
[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
[DefaultPriority(0)]
public class ProductsServiceAvailabilityDataTests : IClassFixture<SharedServiceAvailabilityDataFixture>
{
    private readonly SharedServiceAvailabilityDataFixture fixture;
    private readonly Guid entityId1;
    private readonly Guid entityId2;

    public ProductsServiceAvailabilityDataTests(SharedServiceAvailabilityDataFixture fixture)
    {
        this.fixture = fixture;
        entityId1 = new Guid("6ac53f33-ea5a-47dd-b7f2-1d33bab0a499");
        entityId2 = new Guid("2b652c96-13fc-4d37-b28d-bd2ad63f58ef");
    }

    /// <summary>
    ///     Работает добавление продукта
    /// </summary>
    [Fact]
    [Priority(-100)]
    public async Task AddProductShouldWork()
    {
        // Arrange
        var request1 = TestEntityProvider.Shared.Create<CreateProductRequest>(s => s.Id = entityId1);
        var request2 = TestEntityProvider.Shared.Create<CreateProductRequest>(s => s.Id = entityId2);

        // Act
        await fixture.ProductsService.AddProduct(request1, CancellationToken.None);
        await fixture.ProductsService.AddProduct(request2, CancellationToken.None);

        // Assert
        var products = await fixture.Reader.Read<Product>().ToListAsync();
        products.Should().HaveCount(2);
        products.Select(b => b.Id).Should().Contain(new[]
        {
            entityId1, entityId2,
        });
    }

    /// <summary>
    ///     Работает обновление продукта
    /// </summary>
    [Fact]
    public async Task UpdateProductShouldWork()
    {
        // Arrange
        var updateRequest = TestEntityProvider.Shared.Create<ProductRequest>(s => s.Id = entityId1);

        // Act
        await fixture.ProductsService.UpdateProductAsync(updateRequest, CancellationToken.None);

        // Assert
        var updatedProduct = await fixture.Reader.Read<Product>().FirstOrDefaultAsync(s => s.Id == entityId1);
        updatedProduct.Should().NotBeNull()
                      .And.BeEquivalentTo(updateRequest);
    }

    /// <summary>
    ///     Возвращает список всех продуктов
    /// </summary>
    [Fact]
    public async Task GetAllProductsAsyncShouldReturnValue()
    {
        // Act
        var result = await fixture.ProductsService.GetAllProductsAsync(CancellationToken.None);

        // Assert
        result.Should().NotBeNull()
              .And.NotBeEmpty()
              .And.HaveCount(2);
    }

    /// <summary>
    ///     Возвращает продукт по идентификатору
    /// </summary>
    [Fact]
    public async Task GetProductByIdAsyncShouldReturnValue()
    {
        // Act
        var result = await fixture.ProductsService.GetProductByIdAsync(entityId1, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
    }

    /// <summary>
    ///     Работает удаление продукта
    /// </summary>
    [Fact]
    [Priority(100)]
    public async Task DeleteProductAsyncShouldWork()
    {
        // Act
        await fixture.ProductsService.DeleteProductAsync(entityId2, CancellationToken.None);

        // Assert
        var result = await fixture.Reader.Read<Product>().FirstOrDefaultAsync(s => s.Id == entityId2);
        result.Should().BeEquivalentTo(new
        {
            Id = entityId2,
            IsDeleted = true,
        });
    }
}
