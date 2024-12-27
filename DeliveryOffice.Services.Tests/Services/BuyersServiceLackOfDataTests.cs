using Ahatornn.TestGenerator;
using DeliveryOffice.DataAccess.Tests;
using DeliveryOffice.Services.Abstractions.RequestModels;
using DeliveryOffice.Services.ServiceExceptions.ForBuyer;
using FluentAssertions;
using Xunit;

namespace DeliveryOffice.Services.Tests.Services;

public class BuyersServiceLackOfDataTests : IClassFixture<SharedServiceLackOfDataFixture>
{
    private readonly SharedServiceLackOfDataFixture fixture;

    public BuyersServiceLackOfDataTests(SharedServiceLackOfDataFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public async Task GetAllBuyersAsyncShouldReturnEmpty()
    {
        // Act
        var result = await fixture.BuyerService.GetAllBuyersWithBillsAsync(CancellationToken.None);

        // Assert
        result.Should().NotBeNull()
              .And.BeEmpty();
    }

    [Fact]
    public async Task GetBuyerByIdAsyncShouldThrowSupplierNotFoundException()
    {
        // Act
        var act = () => fixture.BuyerService.GetBuyerByIdWithBillsAsync(Guid.NewGuid(), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BuyerNotFoundException>();
    }

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
