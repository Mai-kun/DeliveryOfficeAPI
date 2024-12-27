using Ahatornn.TestGenerator;
using DeliveryOffice.Core.Models;
using DeliveryOffice.DataAccess.Repositories.Abstractions.Repositories;
using DeliveryOffice.DataAccess.Tests;
using FluentAssertions;
using Xunit;

namespace DeliveryOffice.DataAccess.Repositories.Tests;

/// <summary>
///     Тесты для <see cref="BillReaderRepository" />
/// </summary>
public class BillReaderRepositoryTests : BaseAppContextTest
{
    private readonly IBillReaderRepository repository;

    public BillReaderRepositoryTests()
    {
        repository = new BillReaderRepository(Context);
    }

    /// <summary>
    ///     Возвращает пустой список, если в базе данных нет данных
    /// </summary>
    [Fact]
    public async Task GetAllShouldReturnEmpty()
    {
        // Act
        var result = await repository.GetAllAsync(Token);

        // Assert
        result.Should().BeEmpty();
    }

    /// <summary>
    ///     Возвращает список счетов, если в базе данных есть данные
    /// </summary>
    [Fact]
    public async Task GetAllShouldReturnValues()
    {
        // Arrange
        var bill1 = TestEntityProvider.Shared.Create<Bill>(
            b =>
            {
                b.Supplier = TestEntityProvider.Shared.Create<Supplier>();
                b.Buyer = TestEntityProvider.Shared.Create<Buyer>();
                b.Products = new List<Product>
                {
                    TestEntityProvider.Shared.Create<Product>(),
                    TestEntityProvider.Shared.Create<Product>(p => p.IsDeleted = true),
                    TestEntityProvider.Shared.Create<Product>(),
                };
            });
        var bill2 = TestEntityProvider.Shared.Create<Bill>(b => b.IsDeleted = true);

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

    /// <summary>
    ///     Возвращает null, если счет не найден или удален
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnNull()
    {
        // Arrange
        var id = Guid.NewGuid();
        var bill = TestEntityProvider.Shared.Create<Bill>(
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

    /// <summary>
    ///     Возвращает счет, если он существует и не удален
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnValue()
    {
        // Arrange
        var bill = TestEntityProvider.Shared.Create<Bill>(
            b =>
            {
                b.Supplier = TestEntityProvider.Shared.Create<Supplier>();
                b.Buyer = TestEntityProvider.Shared.Create<Buyer>();
                b.Products = new List<Product>
                {
                    TestEntityProvider.Shared.Create<Product>(),
                    TestEntityProvider.Shared.Create<Product>(p => p.IsDeleted = true),
                    TestEntityProvider.Shared.Create<Product>(),
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
