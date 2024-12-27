using DeliveryOffice.Core.Models;
using DeliveryOffice.DataAccess.Repositories.Abstractions.Repositories;
using FluentAssertions;
using Xunit;
using DeliveryOffice.DataAccess.Tests;

namespace DeliveryOffice.DataAccess.Repositories.Tests;

/// <summary>
///     Тесты для <see cref="SupplierReaderRepository" />
/// </summary>
public class SupplierReaderRepositoryTests : BaseAppContextTest
{
    private readonly ISupplierReaderRepository repository;

    public SupplierReaderRepositoryTests()
    {
        repository = new SupplierReaderRepository(Context);
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
    ///     Возвращает список поставщиков со счетами, если в базе данных есть данные
    /// </summary>
    [Fact]
    public async Task GetAllWithBillsShouldReturnValuesWithBills()
    {
        // Arrange
        var supplier1 = Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Supplier>(
            s =>
            {
                s.Bills = new List<Bill>
                {
                    Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Bill>(),
                };
            });
        var supplier2 = Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Supplier>(s => s.IsDeleted = true);
        var supplier3 = Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Supplier>(
            s =>
            {
                s.Bills = new List<Bill>
                {
                    Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Bill>(bill => bill.IsDeleted = true),
                };
            });

        await Context.AddRangeAsync(supplier1, supplier2, supplier3);
        await Context.SaveChangesAsync(Token);

        // Act
        var result = await repository.GetAllWithBillsAsync(Token);

        // Assert
        result.Should().NotBeEmpty()
              .And.HaveCount(2)
              .And.ContainSingle(s => s.Id == supplier1.Id && s.Bills.Count == 1)
              .And.ContainSingle(s => s.Id == supplier3.Id && s.Bills.Count == 0);
    }

    /// <summary>
    ///     Возвращает null, если поставщик с указанным id не найден
    /// </summary>
    [Fact]
    public async Task GetByIdWithBillsShouldReturnNull()
    {
        // Arrange
        var id = Guid.NewGuid();
        var supplier = Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Supplier>(
            s =>
            {
                s.Id = id;
                s.IsDeleted = true;
            });

        await Context.AddAsync(supplier);
        await Context.SaveChangesAsync(Token);

        // Act
        var result1 = await repository.GetByIdWithBillsAsync(Guid.NewGuid(), Token);
        var result2 = await repository.GetByIdWithBillsAsync(id, Token);

        // Assert
        result1.Should().BeNull();
        result2.Should().BeNull();
    }

    /// <summary>
    ///     Возвращает поставщика со счетами, если поставщик с указанным id найден
    /// </summary>
    [Fact]
    public async Task GetByIdWithBillsShouldReturnValueWithBills()
    {
        // Arrange
        var supplier = Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Supplier>(
            s =>
            {
                s.Bills = new List<Bill>
                {
                    Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Bill>(),
                    Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Bill>(bill => bill.IsDeleted = true),
                    Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Bill>(),
                };
            });

        await Context.AddAsync(supplier);
        await Context.SaveChangesAsync(Token);

        // Act
        var result = await repository.GetByIdWithBillsAsync(supplier.Id, Token);

        // Assert
        result.Should().NotBeNull().And.BeEquivalentTo(supplier, options => options.Excluding(s => s.Bills));
        result!.Bills.Should().NotBeNull().And.HaveCount(2);
    }

    /// <summary>
    ///     Возвращает null, если поставщик с указанным id не найден
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnNull()
    {
        // Arrange
        var id = Guid.NewGuid();
        var supplier = Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Supplier>(
            s =>
            {
                s.Id = id;
                s.IsDeleted = true;
            });

        await Context.AddAsync(supplier);
        await Context.SaveChangesAsync(Token);

        // Act
        var result1 = await repository.GetByIdAsync(Guid.NewGuid(), Token);
        var result2 = await repository.GetByIdAsync(id, Token);

        // Assert
        result1.Should().BeNull();
        result2.Should().BeNull();
    }

    /// <summary>
    ///     Возвращает поставщика, если поставщик с указанным id найден
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnValue()
    {
        // Arrange
        var supplier = Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Supplier>();

        await Context.AddAsync(supplier);
        await Context.SaveChangesAsync(Token);

        // Act
        var result = await repository.GetByIdAsync(supplier.Id, Token);

        // Assert
        result.Should().NotBeNull().And.BeEquivalentTo(supplier);
    }
}
