using Ahatornn.TestGenerator;
using DeliveryOffice.Core.Models;
using DeliveryOffice.DataAccess.Tests;
using DeliveryOffice.Services.Abstractions.RequestModels;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Priority;

namespace DeliveryOffice.Services.Tests.Services;

[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
[DefaultPriority(0)]
public class SuppliersServiceAvailabilityDataTests : IClassFixture<SharedServiceAvailabilityDataFixture>
{
    private readonly SharedServiceAvailabilityDataFixture fixture;
    private readonly Guid entityId1;
    private readonly Guid entityId2;

    public SuppliersServiceAvailabilityDataTests(SharedServiceAvailabilityDataFixture fixture)
    {
        this.fixture = fixture;
        entityId1 = new Guid("6ac53f33-ea5a-47dd-b7f2-1d33bab0a499");
        entityId2 = new Guid("2b652c96-13fc-4d37-b28d-bd2ad63f58ef");
    }

    [Fact]
    [Priority(-100)]
    public async Task AddSupplierShouldWork()
    {
        // Arrange
        var request1 = TestEntityProvider.Shared.Create<CreateSupplierRequest>(s => s.Id = entityId1);
        var request2 = TestEntityProvider.Shared.Create<CreateSupplierRequest>(s => s.Id = entityId2);

        // Act
        await fixture.SuppliersService.AddSupplier(request1, CancellationToken.None);
        await fixture.SuppliersService.AddSupplier(request2, CancellationToken.None);

        // Assert
        var suppliers = await fixture.Reader.Read<Supplier>().ToListAsync();
        suppliers.Should().HaveCount(2);
        suppliers.Select(b => b.Id).Should().Contain(new[]
        {
            entityId1, entityId2,
        });
    }

    [Fact]
    public async Task UpdateSupplierShouldWork()
    {
        // Arrange
        var updateRequest = TestEntityProvider.Shared.Create<SupplierRequest>(s => s.Id = entityId1);

        // Act
        await fixture.SuppliersService.UpdateSupplierAsync(updateRequest, CancellationToken.None);

        // Assert
        var updatedSupplier = await fixture.Reader.Read<Supplier>().FirstOrDefaultAsync(s => s.Id == entityId1);
        updatedSupplier.Should().NotBeNull()
                       .And.BeEquivalentTo(updateRequest);
    }

    [Fact]
    public async Task GetAllSuppliersAsyncShouldReturnValue()
    {
        // Act
        var result = await fixture.SuppliersService.GetAllSuppliersAsync(CancellationToken.None);

        // Assert
        result.Should().NotBeNull()
              .And.NotBeEmpty()
              .And.HaveCount(2);
    }

    [Fact]
    public async Task GetSupplierByIdAsyncShouldReturnValue()
    {
        // Act
        var result = await fixture.SuppliersService.GetSupplierByIdAsync(entityId1, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    [Priority(100)]
    public async Task DeleteSupplierAsyncShouldWork()
    {
        // Act
        await fixture.SuppliersService.DeleteSupplierAsync(entityId2, CancellationToken.None);

        // Assert
        var result = await fixture.Reader.Read<Supplier>().FirstOrDefaultAsync(s => s.Id == entityId2);
        result.Should().BeEquivalentTo(new
        {
            Id = entityId2,
            IsDeleted = true,
        });
    }
}
