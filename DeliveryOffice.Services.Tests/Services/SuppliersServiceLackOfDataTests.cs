using Ahatornn.TestGenerator;
using DeliveryOffice.DataAccess.Tests;
using DeliveryOffice.Services.Abstractions.RequestModels;
using DeliveryOffice.Services.ServiceExceptions.ForSupplier;
using FluentAssertions;
using Xunit;

namespace DeliveryOffice.Services.Tests.Services;

/// <summary>
///     Тесты для <see cref="SuppliersService" /> без данных
/// </summary>
public class SuppliersServiceLackOfDataTests : IClassFixture<SharedServiceLackOfDataFixture>
{
    private readonly SharedServiceLackOfDataFixture fixture;

    public SuppliersServiceLackOfDataTests(SharedServiceLackOfDataFixture fixture)
    {
        this.fixture = fixture;
    }

    /// <summary>
    ///     Возвращает пустой список поставщиков
    /// </summary>
    [Fact]
    public async Task GetAllSuppliersAsyncShouldReturnEmpty()
    {
        // Act
        var result = await fixture.SuppliersService.GetAllSuppliersAsync(CancellationToken.None);

        // Assert
        result.Should().NotBeNull()
              .And.BeEmpty();
    }

    /// <summary>
    ///     Выбрасывает исключение SupplierNotFoundException при запросе несуществующего поставщика
    /// </summary>
    [Fact]
    public async Task GetSupplierByIdAsyncShouldThrowSupplierNotFoundException()
    {
        // Act
        var act = () => fixture.SuppliersService.GetSupplierByIdAsync(Guid.NewGuid(), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<SupplierNotFoundException>();
    }

    /// <summary>
    ///     Выбрасывает исключение SupplierNotFoundException при обновлении несуществующего поставщика
    /// </summary>
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

    /// <summary>
    ///     Выбрасывает исключение SupplierNotFoundException при удалении несуществующего поставщика
    /// </summary>
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
