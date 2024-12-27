using Ahatornn.TestGenerator;
using DeliveryOffice.DataAccess.Tests;
using DeliveryOffice.Services.Abstractions.RequestModels;
using DeliveryOffice.Services.ServiceExceptions.ForBuyer;
using FluentAssertions;
using Xunit;

namespace DeliveryOffice.Services.Tests.Services;

/// <summary>
///     Тесты для <see cref="BuyersService" /> без данных
/// </summary>
public class BuyersServiceLackOfDataTests : IClassFixture<SharedServiceLackOfDataFixture>
{
    private readonly SharedServiceLackOfDataFixture fixture;

    public BuyersServiceLackOfDataTests(SharedServiceLackOfDataFixture fixture)
    {
        this.fixture = fixture;
    }

    /// <summary>
    ///     Возвращает пустой список покупателей
    /// </summary>
    [Fact]
    public async Task GetAllBuyersAsyncShouldReturnEmpty()
    {
        // Act
        var result = await fixture.BuyerService.GetAllBuyersWithBillsAsync(CancellationToken.None);

        // Assert
        result.Should().NotBeNull()
              .And.BeEmpty();
    }

    /// <summary>
    ///     Выбрасывает исключение BuyerNotFoundException при запросе несуществующего покупателя
    /// </summary>
    [Fact]
    public async Task GetBuyerByIdAsyncShouldThrowSupplierNotFoundException()
    {
        // Act
        var act = () => fixture.BuyerService.GetBuyerByIdWithBillsAsync(Guid.NewGuid(), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BuyerNotFoundException>();
    }

    /// <summary>
    ///     Выбрасывает исключение BuyerNotFoundException при обновлении несуществующего покупателя
    /// </summary>
    [Fact]
    public async Task UpdateSupplierShouldThrowSupplierNotFoundException()
    {
        // Arrange
        var request = TestEntityProvider.Shared.Create<BuyerRequest>();

        // Act
        var act = () => fixture.BuyerService.UpdateBuyerAsync(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BuyerNotFoundException>();
    }

    /// <summary>
    ///     Выбрасывает исключение BuyerNotFoundException при удалении несуществующего покупателя
    /// </summary>
    [Fact]
    public async Task DeleteSupplierAsyncShouldThrowSupplierNotFoundException()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var act = () => fixture.BuyerService.DeleteBuyerAsync(id, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BuyerNotFoundException>();
    }
}
