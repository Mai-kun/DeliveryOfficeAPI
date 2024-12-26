using Ahatornn.TestGenerator;
using DeliveryOffice.DataAccess.Tests;
using DeliveryOffice.Services.Abstractions.Models.RequestModels;
using DeliveryOffice.Services.ServiceExceptions.ForBill;
using FluentAssertions;
using Xunit;

namespace DeliveryOffice.Services.Tests.Services;

public class BillsServiceLackOfDataTests : IClassFixture<SharedServiceLackOfDataFixture>
{
    private readonly SharedServiceLackOfDataFixture fixture;

    public BillsServiceLackOfDataTests(SharedServiceLackOfDataFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public async Task GetAllBillsAsyncShouldReturnEmpty()
    {
        // Act
        var result = await fixture.BillService.GetAllBillsAsync(CancellationToken.None);

        // Assert
        result.Should().NotBeNull()
              .And.BeEmpty();
    }

    [Fact]
    public async Task GetBillByIdAsyncShouldThrowBillNotFoundException()
    {
        // Act
        var act = () => fixture.BillService.GetBillByIdAsync(Guid.NewGuid(), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BillNotFoundException>();
    }

    [Fact]
    public async Task UpdateBillShouldThrowBillNotFoundException()
    {
        // Arrange
        var request = TestEntityProvider.Shared.Create<BillRequest>();

        // Act
        var act = () => fixture.BillService.UpdateBillAsync(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BillNotFoundException>();
    }

    [Fact]
    public async Task DeleteBillAsyncShouldThrowBillNotFoundException()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var act = () => fixture.BillService.DeleteBillAsync(id, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BillNotFoundException>();
    }
}
