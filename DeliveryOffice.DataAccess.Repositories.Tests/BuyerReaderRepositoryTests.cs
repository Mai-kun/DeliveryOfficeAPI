using DeliveryOffice.Core.Models;
using DeliveryOffice.DataAccess.Repositories.Abstractions.Repositories;
using DeliveryOffice.DataAccess.Tests;
using FluentAssertions;
using Xunit;

namespace DeliveryOffice.DataAccess.Repositories.Tests;

/// <summary>
///     Тесты для <see cref="BuyerReaderRepository" />
/// </summary>
public class BuyerReaderRepositoryTests : BaseAppContextTest
{
    private readonly IBuyerReaderRepository repository;

    public BuyerReaderRepositoryTests()
    {
        repository = new BuyerReaderRepository(Context);
    }

    /// <summary>
    ///     Возвращает пустой список, если в базе данных нет данных
    /// </summary>
    [Fact]
    public async Task GetAllWithBillsShouldReturnEmpty()
    {
        // Act
        var result = await repository.GetAllWithBillsAsync(Token);

        // Assert
        result.Should().BeEmpty();
    }

    /// <summary>
    ///     Возвращает список покупателей со счетами, если в базе данных есть данные
    /// </summary>
    [Fact]
    public async Task GetAllWithBillsShouldReturnValuesWithBills()
    {
        // Arrange
        var buyer1 = Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Buyer>(
            b =>
            {
                b.Bills = new List<Bill>
                {
                    Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Bill>(),
                };
            });
        var buyer2 = Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Buyer>(b => b.IsDeleted = true);
        var buyer3 = Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Buyer>(
            b =>
            {
                b.Bills = new List<Bill>
                {
                    Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Bill>(bill => bill.IsDeleted = true),
                };
            });

        await Context.AddRangeAsync(buyer1, buyer2, buyer3);
        await Context.SaveChangesAsync(Token);

        // Act
        var result = await repository.GetAllWithBillsAsync(Token);

        // Assert
        result.Should().NotBeEmpty()
              .And.HaveCount(2)
              .And.ContainSingle(b => b.Id == buyer1.Id && b.Bills.Count == 1)
              .And.ContainSingle(b => b.Id == buyer3.Id && b.Bills.Count == 0);
    }

    /// <summary>
    ///     Возвращает null, если покупатель не найден или удален
    /// </summary>
    [Fact]
    public async Task GetByIdWithBillShouldReturnNull()
    {
        // Arrange
        var id = Guid.NewGuid();
        var buyer = Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Buyer>(
            b =>
            {
                b.Id = id;
                b.IsDeleted = true;
            });

        await Context.AddAsync(buyer);
        await Context.SaveChangesAsync(Token);

        // Act
        var result1 = await repository.GetByIdWithBillAsync(Guid.NewGuid(), Token);
        var result2 = await repository.GetByIdWithBillAsync(id, Token);

        // Assert
        result1.Should().BeNull();
        result2.Should().BeNull();
    }

    /// <summary>
    ///     Возвращает покупателя со счетами, если он существует и не удален
    /// </summary>
    [Fact]
    public async Task GetByIdWithBillShouldReturnValueWithBills()
    {
        // Arrange
        var buyer = Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Buyer>(
            b =>
            {
                b.Bills = new List<Bill>
                {
                    Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Bill>(),
                    Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Bill>(bill => bill.IsDeleted = true),
                    Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Bill>(),
                };
            });

        await Context.AddAsync(buyer);
        await Context.SaveChangesAsync(Token);

        // Act
        var result = await repository.GetByIdWithBillAsync(buyer.Id, Token);

        // Assert
        result.Should().NotBeNull().And.BeEquivalentTo(buyer, options => options.Excluding(b => b.Bills));
        result!.Bills.Should().NotBeNull().And.HaveCount(2);
    }

    /// <summary>
    ///     Возвращает null, если покупатель не найден или удален
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnNull()
    {
        // Arrange
        var id = Guid.NewGuid();
        var buyer = Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Buyer>(
            b =>
            {
                b.Id = id;
                b.IsDeleted = true;
            });

        await Context.AddAsync(buyer);
        await Context.SaveChangesAsync(Token);

        // Act
        var result1 = await repository.GetByIdAsync(Guid.NewGuid(), Token);
        var result2 = await repository.GetByIdAsync(id, Token);

        // Assert
        result1.Should().BeNull();
        result2.Should().BeNull();
    }

    /// <summary>
    ///     Возвращает покупателя, если он существует и не удален
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnValue()
    {
        // Arrange
        var buyer = Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Buyer>();

        await Context.AddAsync(buyer);
        await Context.SaveChangesAsync(Token);

        // Act
        var result = await repository.GetByIdAsync(buyer.Id, Token);

        // Assert
        result.Should().NotBeNull().And.BeEquivalentTo(buyer);
    }
}
