using DeliveryOffice.Core.Models;
using DeliveryOffice.DataAccess.Repositories.Abstractions.Repositories;
using FluentAssertions;
using Xunit;
using DeliveryOffice.DataAccess.Tests;

namespace DeliveryOffice.DataAccess.Repositories.Tests;

public class SupplierReaderRepositoryTests : BaseAppContextTest
{
    private readonly ISupplierReaderRepository repository;

    public SupplierReaderRepositoryTests()
    {
        repository = new SupplierReaderRepository(Context);
    }

    [Fact]
    public async Task GetAllShouldReturnEmpty()
    {
        // Act
        var result = await repository.GetAllWithBillsAsync(Token);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllShouldReturnValue()
    {
        // Arrange
        var supplier1 = Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Supplier>();
        var supplier2 = Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Supplier>(s => s.IsDeleted = true);
        var supplier3 = Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Supplier>();
        await Context.AddRangeAsync(supplier1, supplier2, supplier3);
        await Context.SaveChangesAsync(Token);

        // Act
        var result = await repository.GetAllWithBillsAsync(Token);

        // Assert
        result.Should().NotBeEmpty()
            .And.HaveCount(2)
            .And.ContainSingle(s => s.Id == supplier1.Id)
            .And.ContainSingle(s => s.Id == supplier3.Id);
    }


}
