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
///     Тесты для <see cref="BuyersService" /> при работе с данными
/// </summary>
[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
[DefaultPriority(0)]
public class BuyersServiceAvailabilityDataTests : IClassFixture<SharedServiceAvailabilityDataFixture>
{
    private readonly SharedServiceAvailabilityDataFixture fixture;
    private readonly Guid entityId1;
    private readonly Guid entityId2;

    public BuyersServiceAvailabilityDataTests(SharedServiceAvailabilityDataFixture fixture)
    {
        this.fixture = fixture;
        entityId1 = new Guid("6ac53f33-ea5a-47dd-b7f2-1d33bab0a499");
        entityId2 = new Guid("2b652c96-13fc-4d37-b28d-bd2ad63f58ef");
    }

    /// <summary>
    ///     Работает добавление покупателя
    /// </summary>
    [Fact]
    [Priority(-100)]
    public async Task AddBuyerAsyncShouldWork()
    {
        // Arrange
        var request1 = TestEntityProvider.Shared.Create<CreateBuyerRequest>(s => s.Id = entityId1);
        var request2 = TestEntityProvider.Shared.Create<CreateBuyerRequest>(s => s.Id = entityId2);

        // Act
        await fixture.BuyerService.AddBuyerAsync(request1, CancellationToken.None);
        await fixture.BuyerService.AddBuyerAsync(request2, CancellationToken.None);

        // Assert
        var buyers = await fixture.Reader.Read<Buyer>().ToListAsync();
        buyers.Should().HaveCount(2);
        buyers.Select(b => b.Id).Should().Contain(new[]
        {
            entityId1, entityId2,
        });
    }

    /// <summary>
    ///     Работает обновление покупателя
    /// </summary>
    [Fact]
    public async Task UpdateBuyerAsyncShouldWork()
    {
        // Arrange
        var updateRequest = TestEntityProvider.Shared.Create<BuyerRequest>(s => s.Id = entityId1);

        // Act
        await fixture.BuyerService.UpdateBuyerAsync(updateRequest, CancellationToken.None);

        // Assert
        var updatedBuyer = await fixture.Reader.Read<Buyer>().FirstOrDefaultAsync(s => s.Id == entityId1);
        updatedBuyer.Should().NotBeNull()
                    .And.BeEquivalentTo(updateRequest);
    }

    /// <summary>
    ///     Возвращает список всех покупателей
    /// </summary>
    [Fact]
    public async Task GetAllBuyersAsyncShouldReturnValue()
    {
        // Act
        var result = await fixture.BuyerService.GetAllBuyersWithBillsAsync(CancellationToken.None);

        // Assert
        result.Should().NotBeNull()
              .And.NotBeEmpty()
              .And.HaveCount(2);
    }

    /// <summary>
    ///     Возвращает покупателя по ID
    /// </summary>
    [Fact]
    public async Task GetBuyerByIdAsyncShouldReturnValue()
    {
        // Act
        var result = await fixture.BuyerService.GetBuyerByIdWithBillsAsync(entityId1, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
    }

    /// <summary>
    ///     Работает удаление покупателя
    /// </summary>
    [Fact]
    [Priority(100)]
    public async Task DeleteBuyerAsyncShouldWork()
    {
        // Act
        await fixture.BuyerService.DeleteBuyerAsync(entityId2, CancellationToken.None);

        // Assert
        var result = await fixture.Reader.Read<Buyer>().FirstOrDefaultAsync(s => s.Id == entityId2);
        result.Should().BeEquivalentTo(new
        {
            Id = entityId2,
            IsDeleted = true,
        });
    }
}
