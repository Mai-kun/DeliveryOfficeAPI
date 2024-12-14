using DeliveryOffice.Core.RequestModels;
using DeliveryOffice.Services.Validators.Supplier;
using FluentValidation.TestHelper;
using Xunit;

namespace DeliveryOffice.Services.Tests.Validators
{
    /// <summary>
    /// Тесты для <see cref="CreateSupplierRequestValidator"/>
    /// </summary>
    public class CreateSupplierRequestValidatorTests
    {
        private readonly CreateSupplierRequestValidator validator;

        public CreateSupplierRequestValidatorTests()
        {
            validator = new CreateSupplierRequestValidator();
        }

        /// <summary>
        /// Проверяет наличие ошибок при превышении максимальной длины
        /// </summary>
        [Fact]
        public void ShouldHaveValidationErrorForMaximumLength()
        {
            // Arrange
            var model = new CreateSupplierRequest
            {
                Name= new string('*', 300),
            };

            //Act
            var result = validator.TestValidate(model);

            //Assert
            result.ShouldHaveValidationErrorFor(model => model.Name);
            result.ShouldHaveValidationErrorFor(model => model.Address);
        }

        /// <summary>
        /// Проверяет наличие ошибок при null
        /// </summary>
        [Fact]
        public void ShouldHaveValidationErrorForNull()
        {
            // Arrange
            var model = new CreateSupplierRequest
            {
                Name = null,
                Address = null,
            };

            //Act
            var result = validator.TestValidate(model);

            //Assert
            result.ShouldHaveValidationErrorFor(model => model.Name);
            result.ShouldHaveValidationErrorFor(model => model.Address);
        }

        /// <summary>
        /// Проверяет наличие ошибок при пустых данных
        /// </summary>
        [Fact]
        public void ShouldHaveValidationErrorForEmpty()
        {
            // Arrange
            var model = new CreateSupplierRequest
            {
                Name = string.Empty,
                Address = string.Empty,
            };

            //Act
            var result = validator.TestValidate(model);

            //Assert
            result.ShouldHaveValidationErrorFor(model => model.Name);
            result.ShouldHaveValidationErrorFor(model => model.Address);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок
        /// </summary>

        [Fact]
        public void ShouldHaveNoErrors()
        {
            // Arrange
            var model = new CreateSupplierRequest
            {
                Name = "name",
                Address = "address",
            };

            //Act
            var result = validator.TestValidate(model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(model => model.Name);
            result.ShouldNotHaveValidationErrorFor(model => model.Address);
        }
    }
}
