using DeliveryOffice.Services.Abstractions.Models.RequestModels;
using DeliveryOffice.Services.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace DeliveryOffice.Services.Tests.Validators;

/// <summary>
///     Тесты для <see cref="ProductRequestValidator" />
/// </summary>
public class ProductRequestValidatorTests
{
    private readonly ProductRequestValidator validator = new();

    /// <summary>
    ///     Получение ошибки при превышении ограничения строки
    /// </summary>
    [Fact]
    public void ShouldHaveValidationErrorForMaximumLength()
    {
        var model = new ProductRequest
        {
            Name = new string('a', 300),
            Unit = new string('a', 100),
        };

        //Act
        var result = validator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(member => member.Name);
        result.ShouldHaveValidationErrorFor(member => member.Unit);
    }

    /// <summary>
    ///     Получение ошибки при работе с null
    /// </summary>
    [Fact]
    public void ShouldHaveValidationErrorForNull()
    {
        var model = new ProductRequest
        {
            Name = null,
            Unit = null,
        };

        //Act
        var result = validator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(member => member.Name);
        result.ShouldHaveValidationErrorFor(member => member.Unit);
    }

    /// <summary>
    ///     Получение ошибки при пустой строки
    /// </summary>
    [Fact]
    public void ShouldHaveValidationErrorForEmpty()
    {
        var model = new ProductRequest
        {
            Name = string.Empty,
            Unit = string.Empty,
        };

        //Act
        var result = validator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(member => member.Name);
        result.ShouldHaveValidationErrorFor(member => member.Unit);
    }

    /// <summary>
    ///     Получение ошибки на минимальное ограничение строки
    /// </summary>
    [Fact]
    public void ShouldHaveValidationErrorForMinimumValue()
    {
        var model = new ProductRequest
        {
            Quantity = -100,
            Price = -50,
        };

        //Act
        var result = validator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(member => member.Quantity);
        result.ShouldHaveValidationErrorFor(member => member.Price);
    }

    /// <summary>
    ///     Отсутствие ошибки при корректных данных
    /// </summary>
    [Fact]
    public void ShouldHaveNoErrors()
    {
        var model = new ProductRequest
        {
            Name = "Platonist",
            Quantity = 50,
            Unit = "kg",
            Price = 25,
        };

        //Act
        var result = validator.TestValidate(model);

        //Assert
        result.ShouldNotHaveValidationErrorFor(member => member.Name);
        result.ShouldNotHaveValidationErrorFor(member => member.Quantity);
        result.ShouldNotHaveValidationErrorFor(member => member.Unit);
        result.ShouldNotHaveValidationErrorFor(member => member.Price);
    }
}
