using DeliveryOffice.Core.Models;
using DeliveryOffice.DataAccess.Repositories.Abstractions.Repositories;
using DeliveryOffice.DataAccess.Tests;
using FluentAssertions;
using Xunit;

namespace DeliveryOffice.DataAccess.Repositories.Tests
{
    public class ProductReaderRepositoryTests : BaseAppContextTest
    {
        private readonly IProductReaderRepository repository;

        public ProductReaderRepositoryTests()
        {
            repository = new ProductReaderRepository(Context);
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
            var product1 = Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Product>();
            var product2 = Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Product>(p => p.IsDeleted = true);
            var product3 = Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Product>();
            await Context.AddRangeAsync(product1, product2, product3);
            await Context.SaveChangesAsync(Token);

            // Act
            var result = await repository.GetAllAsync(Token);

            // Assert
            result.Should().NotBeEmpty()
                  .And.HaveCount(2)
                  .And.ContainSingle(p => p.Id == product1.Id)
                  .And.ContainSingle(p => p.Id == product3.Id);
        }

        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            var product = Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Product>(
                p =>
                {
                    p.Id = id;
                    p.IsDeleted = true;
                });
            await Context.AddAsync(product);
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
            var product = Ahatornn.TestGenerator.TestEntityProvider.Shared.Create<Product>();
            await Context.AddAsync(product);
            await Context.SaveChangesAsync(Token);

            // Act
            var result = await repository.GetByIdAsync(product.Id, Token);

            // Assert
            result.Should().NotBeNull().And.BeEquivalentTo(product);
        }
    }
}
