using Ahatornn.TestGenerator;
using DeliveryOffice.DataAccess.Tests;
using DeliveryOffice.Services.Abstractions.Models.RequestModels;
using DeliveryOffice.Services.ServiceExceptions.ForProduct;
using FluentAssertions;
using Xunit;

namespace DeliveryOffice.Services.Tests.Services;

public class ProductsServiceLackOfDataTests : IClassFixture<SharedServiceLackOfDataFixture>
{
    private readonly SharedServiceLackOfDataFixture fixture;

    public ProductsServiceLackOfDataTests(SharedServiceLackOfDataFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public async Task GetAllProductsAsyncShouldReturnEmpty()
    {
        // Act
        var result = await fixture.ProductsService.GetAllProductsAsync(CancellationToken.None);

        // Assert
        result.Should().NotBeNull()
              .And.BeEmpty();
    }

    [Fact]
    public async Task GetProductByIdAsyncShouldThrowSupplierNotFoundException()
    {
        // Act
        var act = () => fixture.ProductsService.GetProductByIdAsync(Guid.NewGuid(), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ProductNotFoundException>();
    }

    [Fact]
    public async Task UpdateProductShouldThrowProductNotFoundException()
    {
        // Arrange
        var request = TestEntityProvider.Shared.Create<ProductRequest>();

        // Act
        var act = () => fixture.ProductsService.UpdateProductAsync(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ProductNotFoundException>();
    }

    [Fact]
    public async Task DeleteProductAsyncShouldThrowProductNotFoundException()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var act = () => fixture.ProductsService.DeleteProductAsync(id, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ProductNotFoundException>();
    }
}
