using DeliveryOffice.Core.Models;
using DeliveryOffice.DataAccess.Repositories.Abstractions.Repositories;
using DeliveryOffice.DataAccess.Tests;
using FluentAssertions;
using FluentAssertions.Equivalency;
using Xunit;

namespace DeliveryOffice.DataAccess.Repositories.Tests;

public class BillReaderRepositoryTests : BaseAppContextTest
{
    private readonly IBillReaderRepository repository;

    public BillReaderRepositoryTests()
    {
        repository = new BillReaderRepository(Context);
    }

    [Fact]
    public async Task GetAllShouldReturnEmpty()
    {
        // Act
        var result = await repository.GetAllAsync(Token);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllShouldReturnValues()
    {
        // Arrange
        var bill1 = Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Bill>(
            b =>
            {
                b.Supplier = Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Supplier>();
                b.Buyer = Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Buyer>();
                b.Products = new List<Product>
                {
                    Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Product>(),
                    Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Product>(p => p.IsDeleted = true),
                    Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Product>(),
                };
            });
        var bill2 = Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Bill>(b => b.IsDeleted = true);

        await Context.AddRangeAsync(bill1, bill2);
        await Context.SaveChangesAsync(Token);

        // Act
        var result = await repository.GetAllAsync(Token);

        // Assert
        result.Should().NotBeEmpty()
              .And.HaveCount(1)
              .And.ContainSingle(b => b.Id == bill1.Id &&
                                      b.Products.Count == 3 &&
                                      b.Supplier!.Id == bill1.Supplier!.Id &&
                                      b.Buyer!.Id == bill1.Buyer!.Id);
    }

    [Fact]
    public async Task GetByIdShouldReturnNull()
    {
        // Arrange
        var id = Guid.NewGuid();
        var bill = Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Bill>(
            b =>
            {
                b.Id = id;
                b.IsDeleted = true;
            });

        await Context.AddAsync(bill);
        await Context.SaveChangesAsync(Token);

        // Act
        var result1 = await repository.GetByIdAsync(Guid.NewGuid(), Token);
        var result2 = await repository.GetByIdAsync(id, Token);

        // Assert
        result1.Should().BeNull();
        result2.Should().BeNull();
    }

    [Fact]
    public async Task GetByIdShouldReturnValue()
    {
        // Arrange
        var bill = Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Bill>(
            b =>
            {
                b.Supplier = Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Supplier>();
                b.Buyer = Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Buyer>();
                b.Products = new List<Product>
                {
                    Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Product>(),
                    Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Product>(p => p.IsDeleted = true),
                    Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Product>(),
                };
            });

        await Context.AddAsync(bill);
        await Context.SaveChangesAsync(Token);

        // Act
        var result = await repository.GetByIdAsync(bill.Id, Token);

        // Assert
        result.Should().NotBeNull()
              .And.BeEquivalentTo(bill, options => options.Excluding(x => x.Products)
                                                          .Excluding(x => x.Supplier)
                                                          .Excluding(x => x.Buyer));
        result!.Products.Should().NotBeNull().And.HaveCount(3);
        result.Supplier.Should().NotBeNull();
        result.Buyer.Should().NotBeNull();
    }
}
