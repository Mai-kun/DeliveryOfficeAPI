using Ahatornn.TestGenerator;
using DeliveryOffice.DataAccess.Tests;
using DeliveryOffice.Services.Abstractions.Models.RequestModels;
using DeliveryOffice.Services.ServiceExceptions.ForSupplier;
using FluentAssertions;
using Xunit;

namespace DeliveryOffice.Services.Tests;

public class SuppliersServiceLackOfDataTests : IClassFixture<SharedServiceLackOfDataFixture>
{
    private readonly SharedServiceLackOfDataFixture fixture;

    public SuppliersServiceLackOfDataTests(SharedServiceLackOfDataFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public async Task GetAllSuppliersAsyncShouldReturnEmpty()
    {
        // Act
        var result = await fixture.SuppliersService.GetAllSuppliersAsync(CancellationToken.None);

        // Assert
        result.Should().NotBeNull()
              .And.BeEmpty();
    }

    [Fact]
    public async Task GetSupplierByIdAsyncShouldThrowSupplierNotFoundException()
    {
        // Act
        var act = () => fixture.SuppliersService.GetSupplierByIdAsync(Guid.NewGuid(), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<SupplierNotFoundException>();
    }

    [Fact]
    public async Task UpdateSupplierShouldThrowSupplierNotFoundException()
    {
        // Arrange
        var request = TestEntityProvider.Shared.Create<SupplierRequest>();

        // Act
        var act = () => fixture.SuppliersService.UpdateSupplierAsync(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<SupplierNotFoundException>();
    }

    [Fact]
    public async Task DeleteSupplierAsyncShouldThrowSupplierNotFoundException()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var act = () => fixture.SuppliersService.DeleteSupplierAsync(id, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<SupplierNotFoundException>();
    }
}
