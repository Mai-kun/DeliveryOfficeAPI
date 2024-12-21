using DeliveryOffice.Services.Abstractions.Models.RequestModels;
using DeliveryOffice.Services.Validators.Supplier;
using FluentValidation.TestHelper;
using Xunit;

namespace DeliveryOffice.Services.Tests.Validators;

/// <summary>
///     Тесты для <see cref="CreateSupplierRequestValidator" />
/// </summary>
public class CreateSupplierRequestValidatorTests
{
    private readonly CreateSupplierRequestValidator validator = new();

    /// <summary>
    ///     Получение ошибки при превышении ограничения строки
    /// </summary>
    [Fact]
    public void ShouldHaveValidationErrorForMaximumLength()
    {
        // Arrange
        var model = new CreateSupplierRequest { Name = new string('*', 300), };

        //Act
        var result = validator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(member => member.Name);
        result.ShouldHaveValidationErrorFor(member => member.Address);
    }

    /// <summary>
    ///     Получение ошибки при работе с null
    /// </summary>
    [Fact]
    public void ShouldHaveValidationErrorForNull()
    {
        // Arrange
        var model = new CreateSupplierRequest { Name = null, Address = null, };

        //Act
        var result = validator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(member => member.Name);
        result.ShouldHaveValidationErrorFor(member => member.Address);
    }

    /// <summary>
    ///     Получение ошибки при пустой строки
    /// </summary>
    [Fact]
    public void ShouldHaveValidationErrorForEmpty()
    {
        // Arrange
        var model = new CreateSupplierRequest { Name = string.Empty, Address = string.Empty, };

        //Act
        var result = validator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(member => member.Name);
        result.ShouldHaveValidationErrorFor(member => member.Address);
    }

    /// <summary>
    ///     Отсутствие ошибки при корректных данных
    /// </summary>
    [Fact]
    public void ShouldHaveNoErrors()
    {
        // Arrange
        var model = new CreateSupplierRequest { Name = "name", Address = "address", };

        //Act
        var result = validator.TestValidate(model);

        //Assert
        result.ShouldNotHaveValidationErrorFor(member => member.Name);
        result.ShouldNotHaveValidationErrorFor(member => member.Address);
    }
}
