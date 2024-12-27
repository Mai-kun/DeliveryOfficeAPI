using Ahatornn.TestGenerator;
using DeliveryOffice.DataAccess.Tests;
using DeliveryOffice.Services.Abstractions.RequestModels;
using DeliveryOffice.Services.ServiceExceptions.ForProduct;
using FluentAssertions;
using Xunit;

namespace DeliveryOffice.Services.Tests.Services;

/// <summary>
///     Тесты для <see cref="ProductsService" /> без данных
/// </summary>
public class ProductsServiceLackOfDataTests : IClassFixture<SharedServiceLackOfDataFixture>
{
    private readonly SharedServiceLackOfDataFixture fixture;

    public ProductsServiceLackOfDataTests(SharedServiceLackOfDataFixture fixture)
    {
        this.fixture = fixture;
    }

    /// <summary>
    ///     Возвращает пустой список продуктов
    /// </summary>
    [Fact]
    public async Task GetAllProductsAsyncShouldReturnEmpty()
    {
        // Act
        var result = await fixture.ProductsService.GetAllProductsAsync(CancellationToken.None);

        // Assert
        result.Should().NotBeNull()
              .And.BeEmpty();
    }

    /// <summary>
    ///     Выбрасывает исключение ProductNotFoundException при запросе несуществующего продукта
    /// </summary>
    [Fact]
    public async Task GetProductByIdAsyncShouldThrowSupplierNotFoundException()
    {
        // Act
        var act = () => fixture.ProductsService.GetProductByIdAsync(Guid.NewGuid(), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ProductNotFoundException>();
    }

    /// <summary>
    ///     Выбрасывает исключение ProductNotFoundException при обновлении несуществующего продукта
    /// </summary>
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

    /// <summary>
    ///     Выбрасывает исключение ProductNotFoundException при удалении несуществующего продукта
    /// </summary>
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
